Feature: GRS Deal Summary API Feature
  This feature is to verify the Deal Summary API

  @api @regression @Done @Sprint4
  Scenario Outline: Verify whether I get a successful response returned for the deal summary api call
    Given that I am submitting a get request for fetching count of all the status types for the user with <user> access
    Then I receive a response with status as successful for the deal summary request for the user with <user> access
    Examples:
      | user            |
      | vboya            |
      | NPTA            |
      | Actuary         |
      | PTA             |
      | Actuary Manager |
      | UW Manager      |
      | All Access      |


  @api @regression @Done @Sprint4
  Scenario Outline: Verify whether the deal summary request response matches with DB
    Given that I am submitting a get request for fetching count of all the status types for the user with <user> access
    #And I fetch the count of deals from DB for the user with <user> access
    Then the get request response for the user with <user> access and the db query result is matched successfully
    Examples:
      | user            |
      | UW              |
      | NPTA            |
      | Actuary         |
      | PTA             |
      | Actuary Manager |
      | UW Manager      |
      | All Access      |

  @api @regression @Done @Sprint4
  Scenario Outline: Verify whether the deal summary request response matches with the schema provided
    Given that I am submitting a get request for fetching count of all the status types for the user with <user> access
    Then the get request response for the user with <user> access and the schema provided is matched successfully
    Examples:
      | user            |
      | UW              |
      | NPTA            |
      | Actuary         |
      | PTA             |
      | Actuary Manager |
      | UW Manager      |
      | All Access      |