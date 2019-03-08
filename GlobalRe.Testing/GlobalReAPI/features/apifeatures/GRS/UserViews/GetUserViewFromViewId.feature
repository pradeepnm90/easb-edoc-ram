Feature: GRS  API Feature for fetching User View from a specific view id
  This feature is to verify the GET request to fetch a particular user view from viewId


  @api @Sprint10 @Done @regression @GRS743
  Scenario Outline: Verify whether I get a successful response when we fetch a view with a specific view Id
    Given that I am submitting a Userviews API POST request for fetching all the views for screen name <screenName> view name <viewName> and layout <layout>
    Then I verify if I get  response code for userViews POST API as <expectedResponseCodeforPOST>
    And I submit GET request with a specific view ID
    Then I verify if I the response code  for GET request for fetching userView with specific view ID is the expected response code <expectedResponseCode>
    Examples:
      | screenName                 |viewName                    |layout                 |expectedResponseCodeforPOST   |expectedResponseCode       |
      | Automation Screen          |View By Id #timestamp       |Test Layout            |201                           |200                        |

  @api @Sprint10 @Done @regression @GRS743
  Scenario Outline: Verify if response for fetching a view with a specific view Id is matching with the database
    Given that I am submitting a Userviews API POST request for fetching all the views for screen name <screenName> view name <viewName> and layout <layout>
    Then I verify if I get  response code for userViews POST API as <expectedResponseCodeforPOST>
    And I submit GET request with a specific view ID
    Then I verify if I the response code  for GET request for fetching userView with specific view ID is the expected response code <expectedResponseCode>
    And I verify if the response for fetching userView with specific view ID and Database is matched successfully
    Examples:
      | screenName          |viewName                    |layout                 |expectedResponseCodeforPOST   |expectedResponseCode       |
      | Automation          |View By Id #timestamp       |Test Layout            |201                           |200                        |







