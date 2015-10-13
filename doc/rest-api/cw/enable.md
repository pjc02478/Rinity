enable
====
Enables the method which you disabled before.

* __Request__
  * src : entire source code which you wants to modify
  * methodName : method name which you wants to enable
* __Response__
  * src : modified source code 
  
Example
----
__Request__
```json
{
  "src" : "class Foo { void Bar(){} }"
}
```
