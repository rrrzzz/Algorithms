﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Corman.DataStructures
{
    public class HashTable<T>
    {
        private readonly int _acceptableLoad;
        private int ActualLoad => _elementCount / _buckets.Length;
        private int _elementCount;
        private LinkedList<Tuple<int, T>>[] _buckets = new LinkedList<Tuple<int, T>>[11];

        public HashTable() : this(5){}

        public HashTable(int load)
        {
            _acceptableLoad = load;
            InitializeBuckets();
        }

        public void Add(int key, T value)
        {
            _elementCount++;

            if (ActualLoad >= _acceptableLoad)
            {
                ResizeHashTable();
            }

            var bucket = GetBucket(key);

            if (bucket.Any(x => x.Item1 == key))
            {
                throw new ArgumentException($"Key {key} is already present in hashtable.");
            }

            bucket.AddFirst(new Tuple<int, T>(key, value));
        }

        public void Delete(int key)
        {
            _elementCount--;
            var bucket = GetBucket(key);
            var nodeToRemove = FindKeyValuePairByKey(bucket, key);

            bucket.Remove(nodeToRemove);
        }

        public T GetValue(int key)
        {
            var bucket = GetBucket(key);
            foreach (var tuple in bucket)
            {
                if (tuple.Item1 == key)
                {
                    return tuple.Item2;
                }
            }

            throw new ArgumentException($"There is no key {key} in hashtable.");
        }

        public void SetValue(int key, T newValue)
        {
            var bucket = GetBucket(key);
            var listNode = FindKeyValuePairByKey(bucket, key);
            listNode.Value = new Tuple<int, T>(key, newValue);
        }

        private LinkedListNode<Tuple<int,T>> FindKeyValuePairByKey(LinkedList<Tuple<int,T>> bucket, int key)
        {
            var currentNode = bucket.First;
            while (currentNode != null && currentNode.Value.Item1 != key)
            {
                currentNode = currentNode.Next;
            }

            if (currentNode == null)
            {
                throw new ArgumentException($"There is no key {key} in hashtable.");
            }

            return currentNode;
        }

        private LinkedList<Tuple<int, T>> GetBucket(int key)
        {
            return _buckets[GetHash(key)];
        }

        private int GetHash(int inputKey)
        {
            return inputKey % _buckets.Length;
        }

        private void ResizeHashTable()
        {
            var closestPrime = PrimeGenerator.GetNextClosestPrime(_buckets.Length * 2);
            var oldBuckets = _buckets;
            _buckets = new LinkedList<Tuple<int, T>>[closestPrime];
            InitializeBuckets();

            foreach (var bucket in oldBuckets)
            {
                foreach (var tuple in bucket)
                {
                    var newBucket = GetBucket(tuple.Item1);
                    newBucket.AddFirst(tuple);
                }
            }
        }

        private void InitializeBuckets()
        {
            for (int i = 0; i < _buckets.Length; i++)
            {
                _buckets[i] = new LinkedList<Tuple<int, T>>();
            }
        }
    }
}