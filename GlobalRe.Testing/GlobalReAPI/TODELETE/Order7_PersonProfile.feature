Feature: GRS Person Profile API Feature
  This feature is to verify the Person Profile API

  @api @regression @Sprint4 @Done
  Scenario Outline: Verify whether I get a successful response returned for the Person Profile api call
    Given that I am submitting a Person Profile get request for fetching the subdivision details of the user having the access as <user>
    Then I receive a response with status as successful for the Person Profile request for user having the access as <user>
    Examples:
      | user |
      | PTA  |

  @api @Sprint4 @regression @Done
  Scenario Outline: Verify whether the Person Profile request response matches with DB
    Given that I am submitting a Person Profile get request for fetching the subdivision details of the user having the access as <user>
    #And I fetch the Person Profile from DB by <user> access
    Then the Person Profile get request response to and the db query result for the <user> user access is matched successfully
    Examples:
      | user       |
      | PTA        |
      | UW Manager |

  @api @Sprint4 @regression @Done
  Scenario Outline: Verify whether the Person Profile request response matches with the schema provided
    Given that I am submitting a Person Profile get request for fetching the subdivision details of the user having the access as <user>
    Then the Person Profile get request response for user having the access as <user> and the schema provided is matched successfully
    Examples:
      | user |
      | PTA  |