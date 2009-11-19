		public static void SafeFetchEntityCollection(Action fetchAction, params KeyValuePair<IList, IList<FieldCompareRangePredicate>>[] predicateValuesPairs) {
			var pairCount = predicateValuesPairs.Length;
			var maxItems = 1000;
			var totalItems = 0;

			for (int i = 0; i < pairCount && totalItems < maxItems; i++) {
				var pvPair = predicateValuesPairs[i];
				totalItems += pvPair.Key.Count * pvPair.Value.Count;
			}
			if (totalItems < maxItems) { // do bulk fetch
				for (int i = 0; i < predicateValuesPairs.Length; i++) {
					var pvPair = predicateValuesPairs[i];
					var predicates = pvPair.Value;
					var predicateCount = predicates.Count;
					var values = pvPair.Key;
					var valueCount = values.Count;
					for (int predicateIndex = 0; predicateIndex < predicateCount; predicateIndex++) {
						predicates[predicateIndex].Values = CollectionFactory.CreateObjectList(values, values.Count);
					}
				}
				fetchAction();
			} else {
				for (int i = 0; i < predicateValuesPairs.Length; i++) {
					var pvPair = predicateValuesPairs[i];
					var predicates = pvPair.Value;
					var predicateCount = predicates.Count;
					var values = pvPair.Key;
					var valueCount = values.Count;

					if (predicateCount * valueCount < maxItems) { // do semi-bulk fetch
						for (int predicateIndex = 0; predicateIndex < predicateCount; predicateIndex++) {
							predicates[predicateIndex].Values = CollectionFactory.CreateObjectList(values, values.Count);
						}
						fetchAction();
					} else { // do chunk fetch
						for (int predicateIndex = 0; predicateIndex < predicateCount; predicateIndex++) {
							var predicate = predicates[predicateIndex];
							for (int startValueIndex = 0; startValueIndex < valueCount; startValueIndex += valueCount / maxItems) {
								var rangedValueCount = Math.Min(maxItems, valueCount - startValueIndex);
								predicate.Values = CollectionFactory.CreateObjectList(values.GetRangeUntyped(startValueIndex, rangedValueCount), rangedValueCount);
								fetchAction();
							}
						}
					}
				}
			}
		}
