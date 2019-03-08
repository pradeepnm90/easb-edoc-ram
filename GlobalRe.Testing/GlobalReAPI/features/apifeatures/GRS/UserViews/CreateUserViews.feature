Feature: GRS POST User Views API Feature
  This feature is to verify the POST request to create User Views

  @api @Sprint10 @Done @regression @GRS472
  Scenario Outline: Verify whether I get a successful response for the POST Userviews api call
    Given that I am submitting a Userviews API POST request for fetching all the views for screen name <screenName> view name <viewName> and layout <layout>
    Then I verify if I get  response code for userViews POST API as <expectedResponseCodeforPOST>
    And I verify if the view is created successfully in the database
    Examples:
      | screenName            |viewName                        |layout                   |expectedResponseCodeforPOST|
      | Automation            |My Submission #timestamp        |Test Layout              |201                 |


  @api @Sprint10 @Done @regression @GRS742
  Scenario Outline: Verify if I get appropriate error code if mandatory fields are not provided for the POST Userviews api call
    Given that I am submitting a Userviews API POST request for fetching all the views for screen name <screenName> view name <viewName> and layout <layout>
    Then I verify if I get  response code for userViews POST API as <expectedResponseCodeforPOST>
    Examples:
      | screenName            |viewName                        |layout                   |expectedResponseCodeforPOST|
      |                       |My Submission #timestamp        |Test Layout              |400                 |
      | Automation            |                                |Test Layout              |400                 |
      | Automation            |My Submission #timestamp        |                         |400                 |
      |                       |                                |                         |400                    |






