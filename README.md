# WatchsterApp 

Watchster is an web application capable of managing and recomanding movies to its users.  

## Functional Requirements  
- [ ] Users can register into the applicaion
- [ ] The System sends a confirmation email that the account has been created to verify the email
- [ ] Users can login into the application
- [ ] Every authenticated user can see all the listed movies
- [ ] A listed movie should have at least the following displayed properties:
  * Title
  * Cover photo 
  * Rating
- [ ] Any movie can be rated by any user
- [ ] Users can see a list of recomanded movies ordered from the best to worst
- [ ] A movie can be marked by an User as seen
- [ ] A movie that was seen will not be recomaned anymore to that user
- [ ] The System sends emails to the users from time to time to give new movie recomandations
- [ ] A User can disable the email notification
- [ ] Users can logout
- [ ] Users can delete their account
 
## Non-Functional Requirements
- [ ] Provide a WebAPI for the application
- [ ] Add validations for User Registration
- [ ] Add validations for User Authentication
- [ ] Users that were not logged in do not have access to the application features like: List movies, List recomanded movies, Give rate to a movie and so on
- [ ] Request to the API are authenticated using JWTTokens
- [ ] Requests to the WebAPI should not take longer than 10 second
- [ ] If a request to WebAPI is taking longer than 10 seconds, than the request is aborted and the UI will provide the message: "HTTP request took to long to complete."
- [ ] The WebAPI should handle at least 10 requests per second
- [ ] Add validations for User Movie Rating
- [ ] The Movie Recomandation is done using concepts of ML
- [ ] Movie Recomander Algorithm use this [Dataset](https://www.kaggle.com/grouplens/movielens-20m-dataset) for training and testing
- [ ] Training and testing accuracy should be above 70%
- [ ] The System uses IMDb API as Third Party Application
- [ ] Use IMDb API to get the details for a specifiec movie base on the IMDb ID that is stored in the System Database
- [ ] Build a scheduled process that will run weekly to send movie recomandaion to all users that are subscribed
- [ ] Email Notification is done using SendGrid
- [ ] Build a scheduled process that will run daily at 4AM UTC and insert in the database new movies that were released
