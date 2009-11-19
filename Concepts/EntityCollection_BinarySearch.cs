		public static int BinarySearch<T>(this EntityCollection<T> collection, Converter<T, int> comparisonPredicate) where T: EntityBase2{
			int left = 0, right = collection.Count;
			while (left < right) {
				int middle = (left + right)/2;
				int predicateResult = comparisonPredicate(collection[middle]);
				if (predicateResult == 0) {
					return middle;
				}
				if (middle == left) {
					predicateResult = comparisonPredicate(collection[right]);
					if (predicateResult == 0) {
						return right;
					}
					break;
				}
				if (predicateResult > 0) {
					right = middle;
				} else {
					left = middle;
				}
			}
			return -1;
		}

		public static int BinarySearch<TEntity, TField>(this EntityCollection<TEntity> collection, int fieldIndex, TField value) 
			where TEntity : EntityBase2
			where TField : IComparable<TField>
		{
			return collection.BinarySearch(entity => 
				((TField) entity.Fields[fieldIndex].CurrentValue).CompareTo(value)
			);
		}

		public static int BinarySearchByPK<TEntity, TField>(this EntityCollection<TEntity> collection, TField value)
			where TEntity : EntityBase2
			where TField : IComparable<TField> {
			return collection.BinarySearch(entity =>
				((TField)entity.PrimaryKeyFields[0].CurrentValue).CompareTo(value)
			);
		}
