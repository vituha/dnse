using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using Ipreo.Vision.Utils.Collections;

namespace VS.Library.UT.Automapper
{
    public abstract class CudCollectionMapperBase<TKey> : IObjectMapper
    {
        public object Map(ResolutionContext context, IMappingEngineRunner mapper)
        {
            if (context.IsSourceValueNull && mapper.ShouldMapSourceCollectionAsNull(context))
                return (object)null;

            IEntityWithKey<TKey>[] source = ((IEnumerable<IEntityWithKey<TKey>>)context.SourceValue).ToArray();

            Type sourceElementType = TypeHelper.GetElementType(context.SourceType, source);
            Type destinationElementType = TypeHelper.GetElementType(context.DestinationType);
            ICollection<IEntityWithKey<TKey>> destination = (ICollection<IEntityWithKey<TKey>>)context.DestinationValue;

            Merge(source, destination, sourceElementType, destinationElementType, mapper);

            //ClearEnumerable(enumerableFor);
            //int num = 0;
            //foreach (object obj in (IEnumerable<object>)collection)
            //{
            //    ResolutionResult resolutionResult = new ResolutionResult(context.CreateElementContext((TypeMap)null, obj, elementType1, elementType2, num));
            //    TypeMap typeMapFor = mapper.ConfigurationProvider.FindTypeMapFor(resolutionResult, elementType2);
            //    Type sourceElementType = typeMapFor != null ? typeMapFor.SourceType : elementType1;
            //    Type destinationElementType = typeMapFor != null ? typeMapFor.DestinationType : elementType2;
            //    ResolutionContext elementContext = context.CreateElementContext(typeMapFor, obj, sourceElementType, destinationElementType, num);
            //    object mappedValue = mapper.Map(elementContext);
            //    this.SetElementValue(enumerableFor, mappedValue, num);
            //    ++num;
            //}
            return destination;
        }

        private void Merge(IEnumerable<IEntityWithKey<TKey>> source, ICollection<IEntityWithKey<TKey>> destination, Type sourceElementType, Type destinationElementType, IMappingEngineRunner mapper, TypeMap typeMapFor)
        {
            ResolutionResult resolutionResult = new ResolutionResult(context.CreateElementContext((TypeMap)null, obj, elementType1, elementType2, num));
            TypeMap typeMapFor = mapper.ConfigurationProvider.FindTypeMapFor(resolutionResult, destinationElementType);

            var added = new List<IEntityWithKey<TKey>>();
            var deleted = new List<IEntityWithKey<TKey>>();
            foreach (var pair in source.FullJoin(destination).On(s => s.Id, d => d.Id))
            {
                IEntityWithKey<TKey> sourceItem = pair.Outer;
                IEntityWithKey<TKey> destinationItem = pair.Inner;
                if (sourceItem == null)
                {
                    deleted.Add(destinationItem);
                }
                else if (destinationItem == null)
                {
                    ResolutionContext elementContext = new ResolutionContext(typeMapFor, sourceItem, sourceElementType, destinationElementType, null, null);
                    destinationItem = (IEntityWithKey<TKey>) mapper.Map(elementContext);
                    added.Add(destinationItem);
                }
                else
                {
                    ResolutionContext elementContext = new ResolutionContext(typeMapFor, sourceItem, destinationItem, sourceElementType, destinationElementType, null, null);
                    mapper.Map(elementContext);
                }
            }
        }

        protected virtual object GetOrCreateDestinationObject(ResolutionContext context, IMappingEngineRunner mapper, Type destElementType, int sourceLength)
        {
            if (context.DestinationValue != null)
            {
                if (!(context.DestinationValue is Array))
                    return context.DestinationValue;
                Array array = context.DestinationValue as Array;
                if (array != null && array.Length >= sourceLength)
                    return context.DestinationValue;
            }
            return this.CreateDestinationObject(context, destElementType, sourceLength, mapper);
        }

        protected virtual TEnumerable GetEnumerableFor(object destination)
        {
            return (TEnumerable)destination;
        }

        protected virtual void ClearEnumerable(TEnumerable enumerable)
        {
        }

        protected virtual object CreateDestinationObject(ResolutionContext context, Type destinationElementType, int count, IMappingEngineRunner mapper)
        {
            Type destinationType = context.DestinationType;
            if (!destinationType.IsInterface && !destinationType.IsArray)
                return mapper.CreateObject(context);
            return (object)this.CreateDestinationObjectBase(destinationElementType, count);
        }

        public abstract bool IsMatch(ResolutionContext context);

        protected abstract void SetElementValue(TEnumerable destination, object mappedValue, int index);

        protected abstract TEnumerable CreateDestinationObjectBase(Type destElementType, int sourceLength);
    }
}