SharedVersionLabeller
=====================

A Labeller for [CruiseControl.Net] v1.8.5.

This Labeller generates incrementing version numbers with a prefix. These can be shared between
different BuildProjects.

UseCase
=======

Suppose you have three branches and thus three BuildProjects:
* stable, v1.1.100
* rc, v1.2.50
* test, v1.3.200

When you declare the current RC as stable, the source branch for RC will become the next
stable BuildProject. In this case you might keep the version numbers even though it will
be build with a different BuildProject:

* stable, v1.2.50
* rc, v1.3.200
* test, v1.4.1

Configuration
=============

```
<project name="stable">
    <labeller type="SharedVersionLabeller">
        <prefix>1.1.</prefix>
        <stateFileName>C:\CC_Root\State.xml</stateFileName>
    </labeller>
</project>
<project name="rc">
    <labeller type="SharedVersionLabeller">
        <prefix>1.2.</prefix>
        <stateFileName>C:\CC_Root\State.xml</stateFileName>
    </labeller>
</project>
<project name="test">
    <labeller type="SharedVersionLabeller">
        <prefix>1.3.</prefix>
        <stateFileName>C:\CC_Root\State.xml</stateFileName>
    </labeller>
</project>
```
