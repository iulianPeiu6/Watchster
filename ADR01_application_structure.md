# Application structure

Contents:

* [Summary](#summary)
  * [Issue](#issue)
  * [Decision](#decision)
  * [Status](#status)
* [Details](#details)
  * [Assumptions](#assumptions)
  * [Constraints](#constraints)
  * [Alternatives](#alternatives)
  * [Argument](#argument)
  * [Justification](#justification)


## Summary


### Issue

We want to use an architecture that enables us to have maintainable software, as well to be expandable easily and can use different supporting tools and plugins in the different software layers based on different technologies.
  
### Decision

The application should be based on Model, View, and Controller (A three tieres architecture)

### Status

Decided.

## Details

### Assumptions

We want to create an application that is modern, fast, reliable, responsive.

The team has proven skills in implementing MVC model.

### Constraints

None

### Alternatives

- [ ] SOA architecture
- [ ] Three tiers architecture
- [ ] Client/Server architecture

### Argument

- [ ] Application maintainability
- [ ] Application adaptability
- [ ] Application expandability
- [ ] Resources capabilities
- [ ] Implementation time

### Justification

Based on the criteria mentioned, we found that SOA architecture will be difficult to be implemented and will need more time to realize the value which can be obtained quickly by three tires. Moreover, the existing developers are not skilled in SOA architecture.
The client/server architecture doesn't fit the project requirements because we need to implement a UI using a front-end framework such as Angular. Active connections are not desired for greater scalability and stability.
