parse_one
====
Parse a single csharp script.

* __Request__
  * src : C# source to parse 
* __Response__
  * className : identifier of the class
  * methods

Example
----
__Request__
```json
{
 "src" : "class Foo { void Bar(){} }"
}
```
__Response__
```json
{
 "className" : "Foo",
 "methods" : [
  {
   "name" : "Bar",
   "returnType" : "void"
  }
 ]
}
```
