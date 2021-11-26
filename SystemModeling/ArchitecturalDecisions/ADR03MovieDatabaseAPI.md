# Movie Database API

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

We want to use an already well-established API for getting movies from a daily updated movie database.
  
### Decision

We will use the TMDB API.

### Status

Final decision.

## Details

### Assumptions

We want to request different information about newly or already released movies 

### Alternatives

  1. IMDb API

### Argument

- [x] Free to use
- [x] Easy to use, provides extensive documentation
- [x] Able to create multiple requests

### Justification


Based on the criteria mentioned, TMDB API would be the best suited for our needs. It provides a great deal of documentation, it is free and easy to use.
IMDb would have also been a suitable choice but we decided against it due to the financial requirements to use the API.