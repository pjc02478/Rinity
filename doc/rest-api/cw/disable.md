disable
====
Disable the method. All lines of the method will be commented and inserts `return default(RETURN_TYPE);` if return type is not a `void`.

* __Request__
  * src : source code to disable
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
