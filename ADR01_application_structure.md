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

The application should be based on n-layered architecture

### Status

Partially decided.

## Details

### Assumptions

We want to create an application that is modern, fast, reliable, responsive.

The team has proven skills in implementing a multi-layered model.

### Constraints

None

### Alternatives

  1. Onion architecture
  2. MVC architecture
  3. Client/Server architecture

### Argument

- [x] Application maintainability
- [x] Application adaptability
- [x] Application expandability
- [x] Resources capabilities
- [x] Implementation time

### Justification

Based on the criteria mentioned, we found the MVC architecture to be outdated and there is greater value to be obtained quickly by n-layered. Moreover, we wanted to experiment with a different approach in regards to implementing a web application.

The client/server architecture doesn't fit the project requirements because we need to implement a UI using a front-end framework such as Angular. Active connections are not desired for greater scalability and stability.
