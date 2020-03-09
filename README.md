# Uniredis

Simple Redis Client for Unity

This client is not actively maintained and it's still work in progress, but you're free to contribute if you want 🙂

## Example (using the SET command)

```csharp
Uniredis.Set(this, "Hello", "World", (error, result) =>
{
    if (error == null)
    {
        Debug.Log(result);
    }
});
```

## Featuring

* Unity-like APIs
* Tested codebase
  
## Limitations

* Only a database at a time is supported (for now)
  
## Commands Supported

### Strings

- [ ] APPEND
- [ ] BITCOUNT
- [x] SET
- [ ] SETNX
- [ ] SETRANGE
- [ ] STRLEN
- [ ] MSET
- [ ] MSETNX
- [x] GET
- [ ] GETRANGE
- [ ] MGET
- [ ] INCR
- [ ] INCRBY
- [ ] INCRBYFLOAT
- [ ] DECR
- [ ] DECRBY
- [ ] DEL
- [ ] EXPIRE
- [ ] TTL

### Sets

- [ ] SADD
- [ ] SCARD
- [ ] SREM
- [ ] SISMEMBER
- [ ] SMEMBERS
- [ ] SUNION
- [ ] SINTER
- [ ] SMOVE
- [ ] SPOP

### Sorted Sets

- [ ] ZADD
- [ ] ZCARD
- [ ] ZCOUNT
- [ ] ZINCRBY
- [ ] ZRANGE
- [ ] ZRANK
- [ ] ZREM
- [ ] ZREMRANGEBYRANK
- [ ] ZREMRANGEBYSCORE
- [ ] ZSCORE
- [ ] ZRANGEBYSCORE

### Hashes

- [ ] HGET
- [ ] HGETALL
- [ ] HSET
- [ ] HSETNX
- [ ] HMSET
- [ ] HINCRBY
- [ ] HDEL
- [ ] HEXISTS
- [ ] HKEYS
- [ ] HLEN
- [ ] HSTRLEN
- [ ] HVALS

### HyperLogLog

- [ ] PFADD
- [ ] PFCOUNT
- [ ] PFMERGE
- [ ] PSUBSCRIBE
- [ ] PUBSUB
- [ ] PUBLISH
- [ ] PUNSUBSCRIBE
- [ ] SUBSCRIBE
- [ ] UNSUBSCRIBE

### Other

- [ ] KEYS
