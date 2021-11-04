# System Requirements

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