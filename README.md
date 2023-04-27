# Solution Overview

This solution provides a portable .NET standard library containing a utility that creates strings containing incremental lines numbers from `1` up to a provided upper limit or configurable word replacements occurring at the defined repetition points based on a provided set of rules. For example, you could call the utility to emit 15 lines with the word 'Foo' on every 5th line in place of the line number. 

The caller can provide an arbitrary list of rules that define the line number at which to replace the number and the word to be emitted. Multiple rules that result in overlaps will result in multiple words together replacing the number. The words are emitted on applicable lines in the order of the provided rules.

## Example
Given these two rules (in this order):
* emit `Foo` every 3rd line
* emit `Bar` every 5rd line

when called to return 15 lines, the resulting strings would contain the incremental line numbers up to and including 15 except:
* every 3rd line will return `Foo`
* every 5rd line will return `Bar`
* every 15th line will return `FooBar`

Here is the complete sample result:

```
1
2
Foo
4
Bar
Foo
7
8
Foo
Bar
11
Foo
13
14
FooBar
```

# Development Notes

## Further Questions
Before making assumptions about desired behavior beyond those stated in the initial requirements and adding unnecessary code, here are some inquiries I'd make to help clarify requirements.

### *Should the solution allow for no word pattern rules?*
The refactored solution takes an arbitrary list of repetition rules, including none. Should this be supported? This would affect additional validation of input parameters. The solution currently accepts 0 rules, resulting in just the line numbers emitted.

### *Should the solution allow for duplicate rules?*
There's currently no check for duplicate line number rules. This might be an important point of limitation/validation depending on the desired use case(s).

### *Is the order of the combined words important?*
Currently, when multiple words appear in a resulting line, they are emitted in the order of the rules. There is no explicit definition of the order. This inherently puts the responsibility on the caller to provide the rules in the desired word order so the question might not be necessary. Explanation of the behavior in documentation would be important and is mentioned in the overview above.

# Implementation Strategy
The first pass of this solution populated the result lines into a `string` array. This resulted in memory usage exceptions when presented with large result iteration counts.

The refactor uses a `yield return` for `IEnumerable` which provides 2 benefits:
* Entirely eliminates storage of the results in the utility library which removes the memory limitation. The functional limitations are now bound only by the iteration parameter numeric limit. An explicit parameter value exceeding the param's type limit will be caught at compile time. A computed value exceeding the parameter limits will result in a runtime exception on the call to the method.
* Supports "lazy loading" of the results, allowing the caller to abort the iteration early as needed, eliminating unnecessary iterations. This is a common efficiency pattern used in I/O intensive iterative calls (network, file system, database) that may not require completion of a full loop cycle.

# Additional consideration

To enhance the extensibility of this utility, we could consider a refactor that decouples the internal predicate logic. Instead of this logic being fixed to the current modulus check of the line number, the rule object could be extended to support a predicate callback so that each rule could have its own predicate for word emission.