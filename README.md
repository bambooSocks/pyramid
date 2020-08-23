# Pyramid Challenge
### How to use
In the `Main` method of the `Program` class, there are four possible ways to load the pyramid string into the program. First two represent the given pyramids as string constants, that are coded directly in the source code. The other two are loading two files, that are also included in the project, containing these two pyramids. This allows for easy loading of several different pyramids dynamically as they can be stored outside of the source code.

### Decisions along the development
The data structure of the graph was considered in form of objects, however this either complicates the graph creating process considerably or in other way causes much larger space complexity. In the end, an adjacency list has proven to be simpler approach when loading while it didn't add unnecesary space complexity.

### Testing
The all functions of the program are tested using unit tests which are all inside the `Tests` project of the solution. These tests were written in order to act as regression tests.