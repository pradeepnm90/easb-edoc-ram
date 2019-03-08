Feature: GRS GET User Views API Feature
  This feature is to verify the GET request to fetch User Views

  @api @Sprint10 @Done @regression
  Scenario Outline: Verify whether I get a successful response for the GET Userviews api call
    Given that I am submitting a Userviews API GET request for fetching all the views for  screen name <screenName>
    Then I verify if I get  response code for userViews GET API as <expectedResponseCode>
    Examples:
      | screenName             |expectedResponseCode|
      |Automation              |200                 |
      |A#1                     |404                 |
      |                        |400                 |


  @api @Sprint10 @Done @regression
  Scenario Outline: Verify whether the GET Userviews api request response matches with DB
    Given that I am submitting a Userviews API GET request for fetching all the views for  screen name <screenName>
      Then Verify if the Userviews GET request response for for screen name <screenName> and the db query result is matched successfully
      Examples:
        | screenName    |
        | Automation               |


  @api @Sprint10 @Done @regression
  Scenario Outline: Verify whether the GET Userviews request response matches with the schema provided
    Given that I am submitting a Userviews API GET request for fetching all the views for  screen name <screenName>
    Then The UserViews API GET  request response for  screen name <screenName> and the schema provided is matched successfully
    Examples:
      | screenName    |
      | Automation               |

