# Introduction

This file will contain the three code metrics: Class coupling, Cyclomatic complexity and Maintainability Index for the Watchster Application for an easier track of the code's quality during the development of the application.

## The measured code metrics

### Cyclomatic complexity

Counts the number of decisions made in the source code. It's number gives us a sense of how hard the code may be to test, mantain or troubleshoot and how likely it is for it to produce errors

The usual limit for this metric is either 10 or 15 for any project. We aim for a low cyclomatic complexity, at least between 10 and 13 and if we have time to lower it below 10, if possible.


### Class Coupling

Measures how many classes a single class uses, the lower the number, the less likely it is for a software error to occur. It also can be used to predict reusability, maintainability and chageability

Couples are counted through parameters, local variables, return types, method calls, template instantions, base classes, interface implementations, attribute decoration.

The optimal value for this metric is 9, we will aim for a class coupling value less than 15

### Maintainability Index 

Is calculated using this formula : MAX(0, (171 - 5*2 * ln(Halstead Volume) - 0.23 * (Cyclomatic Complexity) - 19.2 * ln(Lines of Code)) * 100 / 171)

It gives tresholds of how well the code could perform and if it could create issues. 

Values between 0 and 9 are flagged with red, meaning the code is very suspicious, extremely troublesome for testing, mantaining, changing and might create many software errors.

Values between 10 and 19 are flagged with yellow, meaning the code is suspicious, troublesome for testing,  mantaining, changing and might create software errors.

Values between 20 and 100 are flagged with green, meaning the code is not suspicious, and depending on value it might be easier to test, mantain and change, with lower chances of producing software errors than the other two flags.

We aim for a maintainability index value between 80 and 100.

## Measured metrics on application's code

### Version 1.0 - System structure & WebAPI design

#### Build and surpress active issues analysis

========== Rebuild All: 12 succeeded, 0 failed, 0 skipped ==========

For now Watchster application successfully builds its structure.

#### Calculate code metrics analysis

| Project | Maintainability Index | Cyclomatic Complexity | Class Coupling | Lines of Source code |Lines of Executable code|
|---------|-----------------------|-----------------------|----------------|----------------------|------------------------|
|Core\Watchster.ApplicationService|100|0|0|0|0|
|Core\Watchster.IMDbService|100|0|0|0|0|
|Core\Watchster.MLUtil|100|0|0|0|0|
|Core\Watchster.SendGridService|100|0|0|0|0|
|Infrastructure\Watchster.DataAccess|100|0|0|0|0|
|Presentation\Watchster.WebApi|82|8|39|82|24|
|Tests\IntegrationTests\Watchster.ImdbService.TestConsole|100|1|2|10|1|
|Tests\IntegrationTests\Watchster.MLUtil.TestConsole|100|1|2|10|1|
|Tests\IntegrationTests\Watchster.SendGridService.TestConsole|100|1|2|10|1|
|Tests\UnitTests\Watchster.IMDb.UnitTests|100|3|4|17|1|
|Tests\UnitTests\Watchster.MLUtil.UnitTests|100|3|4|17|1|
|Tests\UnitTests\Watchster.SendGridService.UnitTests|100|3|4|17|1|

The only part that doesn't respect our aimed metrics is the WebAPI's class coupling value, it being 39, but it's to be expected, as it has to configure all the Backend's services and to configure HTTP request pipeline therefore it needs to couple with a higher number of classes.
