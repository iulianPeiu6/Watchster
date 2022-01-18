# System Requirements

## Functional Requirements  
- [x] Users can register into the application.
- [x] Users can log in to the application.
- [x] Every authenticated user can see all the listed movies.
- [x] Any movie can be rated by any user.
- [x] Users can see a list of recommended movies ordered from the best to worst.
- [ ] A movie can be marked by a User as seen.
- [x] A movie that was ~~seen~~ already rated by a user will not be recommended anymore to that user.
- [x] Subscribed users get emails from time to time with new movie recommendations.
- [ ] A User can disable the email notification.
- [x] Users can logout.
- [ ] Users can delete their account.
 
## Non-Functional Requirements
- [x] Provide a WebAPI for the application.
- [x] Add validations for User Registration.
- [x] Add validations for User Authentication.
- [ ] The System sends a confirmation email that the account has been created to verify the email.
- [x] Users that were not logged in do not have access to the application features like: List movies, List recomanded movies, Give rate to a movie and so on.
- [x] A listed movie should have at least the following displayed properties:
  * Title
  * Cover photo 
  * Genre
  * Release date
  * Rating
- [x] Add validations for User Movie Rating.
- [x] Request to the API are authenticated using JWTTokens.
- [x] Requests to the WebAPI should not take longer than 10 second.
- [x] If a request to WebAPI is taking longer than 10 seconds, than the request is aborted and the UI will provide the message: "HTTP request took to long to complete.".
- [x] The WebAPI should handle at least 10 requests per second.
- [x] The Movie Recommendation is done using concepts of ML.
- [x] Movie Recomander Algorithm use ~~this [Dataset](https://www.kaggle.com/grouplens/movielens-20m-dataset)~~ data from database for training and testing.
- [x] Training and testing accuracy should be above 70%.
- [x] Use ~~IMDB~~ TMDb API to get new launched movies or more details about a movie.
- [x] Use SendGrid to send emails.
- [x] The System uses ~~IMDb~~ TMDb API as Third Party Application.
- [x] Use ~~IMDb~~ TMDb API to get the details for a specifiec movie base on the ~~IMDb~~ TMDb ID that is stored in the System Database.
- [x] Build a scheduled process that will run weekly to send movie recomandaion to all users that are subscribed.
- [x] Email Notification is done using SendGrid.
- [x] Build a scheduled process that will run daily at 4AM UTC and insert in the database new movies that were released.
