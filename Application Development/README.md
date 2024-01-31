EPITECH Area miror

-----------SUBJECT ------------

The goal of this project is to create an interconnected mobile app, Web site and backend to create Action Reaction  between service.

------BACKEND --------

• The following back end consists of  connecting successfully the 3 parts. Please run docker compose up before any manipulation.

• Please check this swagger documentation to see any back request:http://localhost:8080/swagger/index.html

• It is made on c#  asp.net core

------MOBILE APP --------
• The mobile app is the application on telephone of our project.

• The following mobile app have:

    -> A login section 
    -> A create account section 
    -> A homepage section ( with a description of our project, a tiny profile and a list of our area)
    -> An Area section(where to create and view the list of the area)
    -> A profile section where to see user account information
 

• Version 1.0 (25/09):

    -> Login and create account frontend finished

• Version 1.1 (4/10):

    -> Refactor of the ui
    -> Homepage section done with profile, area list and description
    -> Profile front
    -> Docker set

• Version 1.2 (7/10):

    -> Fix of the convenance
    -> Area section done (front only)

• Version 1.3 (15/10):
    -> Instance buying and confirmation of scaleway
    -> Front back link of login create account
    -> Docker running and implémentation of apk docker.
    -> Description édit implémentation
    ->  Logout 
    -> User token and ctx implémentation 

• Version 1.4 (20/10):
    -> Scaleway down and incompatible with the New migration and version of the back
    -> Reverse proxy by ngrock 
    -> Docker build unusable on ngrok reverse
    -> User token and ctx fix
    -> Profile link.
    -> Adding all request available for now
    -> O2auth implementation 
    -> Docker apk not fixed

• Version 1.5(25/10):
    -> Link request and area création
    -> O2auth  still not fix. Error redirection uri and something wrong error with auth.expo.io. 
    -> Docker apk still not fixed

Version 1.6 (05/11):

------ HOW TO USE MOBILE --------

•Create an account

•Login

•Navigate through the page

    -> On hommage click on the button under the cars to be redirected on what you want for more info.
    -> On profile page see your information, connect to service or change tour description.
    -> On area page, see your existing area by scrolling or create your area

• On create your area select the action first THEN the reaction ! It will open a menu where you can chose the service you want your réaction and action  to be done .

• When your service is sélected on the action and réaction menu, a second menu with the list of the action and réaction possible is launch.

• Sélect the action and réaction to be perform and fill the nécessary input text and field then enter the name of the area you want to be called and your User id (check your profile) and the description if you want.

• It is done your area is created, check the list of the area on the left modal.