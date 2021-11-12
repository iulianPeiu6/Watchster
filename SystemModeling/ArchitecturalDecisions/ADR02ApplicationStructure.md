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

The application should be based on Onion architecture, changed from n-layered architecture.

### Status

Final decision.

## Details

### Assumptions

We want to create an application that is modern, fast, reliable, responsive.

The team has know-how regarding the basic concepts of onion architecture.

### Constraints

* Use .NET 5 or 6 for Backend Development.  
* Use Angular/React/Blazor/Razor for Frontend Development

### Alternatives

  1. N-layered architecture
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

The N-layered architecture would have been a viable model but we realized that as the project grows it becomes hard to manage and when an issue occurs in one of the functionality it becomes hard sometimes to identify what the issue is and at which layer it is coming from.

Therefore, we chose Onion architecture for some of the following reasons:

* Onion Architecture layers are connected through interfaces. Implantations are provided during run time.
* Application architecture is built on top of a domain model.
* All external dependency, like database access and service calls, are represented in external layers.
* No dependencies of the Internal layer with external layers.
* Couplings are towards the center.
* Flexible and sustainable and portable architecture.
* No need to create common and shared projects.
* Can be quickly tested because the application core does not depend on anything.
