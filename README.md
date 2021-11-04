# WatchsterApp 

Watchster is a web application capable of managing and recommending movies to its users. It will be built as free software, secure, and easy to use. To know more about the features that need to be implemented, read the following sections. 

## User Stories

|                         |User story                                                                            |Requirements               |Priority    |
|:------------------------|--------------------------------------------------------------------------------------|---------------------------|------------|
|Login on Application|As an existing Watchster user, I want to be able to log into my Watchster account on Web browser.|The system must allow users to log into their account on web browser by entering their email and password.|:red_circle: MUST HAVE|
|Register Account on Application|I want users to be able to register in the Application|The system must allow users to register accounts on web browser by entering their email and password and check the subscption box.<br><br> The System sends a confirmation email that the account has been created to verify the email.|:red_circle: MUST HAVE|
|Reset password on Application|As an existing Watchster use, I want to be able to reset my password|The system must allow users to reset their password by clicking on "I forgot my." label on the login page.|:orange_circle: OPTIONAL|
|See all available movies|As an existing Watchster user, I want to see listed all movies and be able filter them.|The system is capable of listing all movies and provide different ways of filtering. The filtering is done on all displayed properties. <br><br> A listed movie should have at least the following displayed properties: Title, Cover photo, Genre, Release date, Rating.|:red_circle: MUST HAVE|
|Movies rating|As an existing Watchster user, I want to be capable to rate movies.|The System must allow users to rate movies. The rating can be updated or removed.|:red_circle: MUST HAVE|
|See recommended movies|As an existing Watchster user, I want to see a list of recommended movies from best to worst.|The system is capable to recommend movies to its users based on different criteria: rating, user tastes, release year, seen or not.|:red_circle: MUST HAVE|
|Mark movie as seen|As an existing Watchster user, I want be able to mark movies as seen.|The system must allow users to mark movies as seen or not seen(the default is not seen).<br><br> A movie that was seen by a user will not be recommended anymore to that user.|:orange_circle: OPTIONAL|
|Movie recommendations subscription|As an existing Watchster user, I want to subscribe to get movie recommendations from time to time.|The system will send weekly emails with new movies recommendations to its users that are subscribed.<br><br> The system allows its users to subscribe and unsubscribe to get or not movies recommendations.|:orange_circle: OPTIONAL|
|Import new movies|As an existing Watchster user, I would like to see new movies that just got launched be present in this application|The system will import new movies that were launched|:orange_circle: OPTIONAL|
|Delete account|As an existing Watchster user, I would like to have the possibility to delete my account.|The system will allow users to delete their accounts.|:orange_circle: OPTIONAL|
|Logout from application|As an existing Watchster user, I would like to logout if I need to.|The system will allow users to logout from the application.|:red_circle: MUST HAVE|

## Functional Requirements  
- [ ] Users can register into the application.
- [ ] Add validations for User Registration.
- [ ] The System sends a confirmation email that the account has been created to verify the email.
- [ ] Users can log in to the application.
- [ ] Add validations for User Authentication.
- [ ] Every authenticated user can see all the listed movies.
- [ ] A listed movie should have at least the following displayed properties:
  * Title
  * Cover photo 
  * Genre
  * Release date
  * Rating
- [ ] Any movie can be rated by any user.
- [ ] Add validations for User Movie Rating.
- [ ] Users can see a list of recommended movies ordered from the best to worst.
- [ ] A movie can be marked by a User as seen.
- [ ] A movie that was seen will not be recommended anymore to that user.
- [ ] The System sends emails to the users from time to time to give new movie recommendations.
- [ ] A User can disable the email notification.
- [ ] The System uses IMDb API as Third Party Application.
- [ ] Use IMDb API to get the details for a specifiec movie base on the IMDb ID that is stored in the System Database.
- [ ] Build a scheduled process that will run weekly to send movie recomandaion to all users that are subscribed.
- [ ] Email Notification is done using SendGrid.
- [ ] Build a scheduled process that will run daily at 4AM UTC and insert in the database new movies that were released.
- [ ] Users can logout.
- [ ] Users can delete their account.
 
## Non-Functional Requirements
- [ ] Provide a WebAPI for the application.
- [ ] Users that were not logged in do not have access to the application features like: List movies, List recomanded movies, Give rate to a movie and so on.
- [ ] Request to the API are authenticated using JWTTokens.
- [ ] Requests to the WebAPI should not take longer than 10 second.
- [ ] If a request to WebAPI is taking longer than 10 seconds, than the request is aborted and the UI will provide the message: "HTTP request took to long to complete.".
- [ ] The WebAPI should handle at least 10 requests per second.
- [ ] The Movie Recommendation is done using concepts of ML.
- [ ] Movie Recomander Algorithm use this [Dataset](https://www.kaggle.com/grouplens/movielens-20m-dataset) for training and testing.
- [ ] Training and testing accuracy should be above 70%.
- [ ] Use IMDB API to get new launched movies or more details about a movie.
- [ ] Use SendGrid to send emails.

## Languages and Technologies Used  
  1. .NET 5 (latest when the development started) for Backend Development
  2. SQL Server 2019 for Database Management
  3. Angular v12.2.8 (latest when the development started) for the UI Development  

## Project Structure
