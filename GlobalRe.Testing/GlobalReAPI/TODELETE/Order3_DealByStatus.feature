Feature: GRS Deal By Status API Feature
  This feature is to verify the Deal By Status API

#  @api
#  Scenario Outline: Verify whether I get a successful response returned for the deal count by status
#    Given that I am submitting a get request for fetching count of all the <status> deals
#    Then I receive a response with status as successful for the <status> deal count request
#    Examples:
#      |status                   |
#      |On Hold                  |
#      |Bound                    |
#      |In Progress              |
#      |Authorize                |
#      |Under Review             |
#      |Outstanding Quote        |
#      |To Be Declined           |
#      |Bound Pending Data Entry |
#      |Renewable                |
#
#  @api
#  Scenario Outline: Verify whether the deal count by status request response matches with DB
#    Given that I am submitting a get request for fetching count of all the <status> deals
#    And I fetch the count of deals having the status as <status> from DB
#    Then the get request response to fetch the count of deals having <status> and the db query result is matched successfully
#    Examples:
#      |status                   |
#      |On Hold                  |
#      |Bound                    |
#      |In Progress              |
#      |Authorize                |
#      |Under Review             |
#      |Outstanding Quote        |
#      |To Be Declined           |
#      |Bound Pending Data Entry |
#      |Renewable                |
#
#  @api
#  Scenario Outline: Verify whether the deal count by status request response matches with the schema provided
#    Given that I am submitting a get request for fetching count of all the <status> deals
#    Then the get request response to fetch the count of deals having <status> and the schema provided is matched successfully
#    Examples:
#      |status                   |
#      |On Hold                  |
#      |Bound                    |
#      |In Progress              |
#      |Authorize                |
#      |Under Review             |
#      |Outstanding Quote        |
#      |To Be Declined           |
#      |Bound Pending Data Entry |
#      |Renewable                |

  @api
  Scenario: Verify whether I get a error response returned for the deal by status when a invalid status is passed
    Given that I am submitting a get request for fetching all the deals with a invalid status code
    Then I receive a error response mentioning special characters

  @api
  Scenario: Verify whether I get a error response returned for the deal by status when a status with special characters is passed
    Given that I am submitting a get request for fetching all the deals with a invalid status code having special characters
    Then I receive a error response mentioning invalid value

  @api @regression @Sprint4 @Done
  Scenario Outline: Verify whether I get a successful response returned for the deal by status all deal request
    Given that I am submitting a get request for fetching all the <status> deals for the user having <user> access
    Then I receive a response with status as successful for the <status> deal request for the user having <user> access
    Examples:
      | status                   | user            |
      | On Hold                  | UW              |
      | Bound - Pending Actions  | UW              |
      | In Progress              | UW              |
      | Authorize                | UW              |
      | Under Review             | UW              |
      | Outstanding Quote        | UW              |
      | To Be Declined           | UW              |
      | Bound Pending Data Entry | UW              |
      | Renewable - 6 Months     | UW              |
      | On Hold                  | NPTA            |
      | Bound - Pending Actions  | NPTA            |
      | In Progress              | NPTA            |
      | Authorize                | NPTA            |
      | Under Review             | NPTA            |
      | Outstanding Quote        | NPTA            |
      | To Be Declined           | NPTA            |
      | Bound Pending Data Entry | NPTA            |
      | Renewable - 6 Months     | NPTA            |
      | On Hold                  | PTA             |
      | Bound - Pending Actions  | PTA             |
      | In Progress              | PTA             |
      | Authorize                | PTA             |
      | Under Review             | PTA             |
      | Outstanding Quote        | PTA             |
      | To Be Declined           | PTA             |
      | Bound Pending Data Entry | PTA             |
      | Renewable - 6 Months     | PTA             |
      | On Hold                  | Actuary Manager |
      | Bound - Pending Actions  | Actuary Manager |
      | In Progress              | Actuary Manager |
      | Authorize                | Actuary Manager |
      | Under Review             | Actuary Manager |
      | Outstanding Quote        | Actuary Manager |
      | To Be Declined           | Actuary Manager |
      | Bound Pending Data Entry | Actuary Manager |
      | Renewable - 6 Months     | Actuary Manager |
      | On Hold                  | UW Manager      |
      | Bound - Pending Actions  | UW Manager      |
      | In Progress              | UW Manager      |
      | Authorize                | UW Manager      |
      | Under Review             | UW Manager      |
      | Outstanding Quote        | UW Manager      |
      | To Be Declined           | UW Manager      |
      | Bound Pending Data Entry | UW Manager      |
      | Renewable - 6 Months     | UW Manager      |
      | On Hold                  | Actuary         |
      | Bound - Pending Actions  | Actuary         |
      | In Progress              | Actuary         |
      | Authorize                | Actuary         |
      | Under Review             | Actuary         |
      | Outstanding Quote        | Actuary         |
      | To Be Declined           | Actuary         |
      | Bound Pending Data Entry | Actuary         |
      | Renewable - 6 Months     | Actuary         |
      | On Hold                  | All Access      |
      | Bound - Pending Actions  | All Access      |
      | In Progress              | All Access      |
      | Authorize                | All Access      |
      | Under Review             | All Access      |
      | Outstanding Quote        | All Access      |
      | To Be Declined           | All Access      |
      | Bound Pending Data Entry | All Access      |
      | Renewable - 6 Months     | All Access      |

  @api @regression @Sprint4 @Done
  Scenario Outline: Verify whether all deals fetched by get request matches with db result
    Given that I am submitting a get request for fetching all the <status> deals for the user having <user> access
    #And I fetch the list of deals having the status as <status> from DB
    Then the <status> get request response for the user having <user> access and the db query result is matched successfully
    Examples:
      | status                   | user            |
      | On Hold                  | UW              |
      | Bound - Pending Actions  | UW              |
      | In Progress              | UW              |
      | Authorize                | UW              |
      | Under Review             | UW              |
      | Outstanding Quote        | UW              |
      | To Be Declined           | UW              |
      | Bound Pending Data Entry | UW              |
      | Renewable - 6 Months     | UW              |
      | On Hold                  | NPTA            |
      | Bound - Pending Actions  | NPTA            |
      | In Progress              | NPTA            |
      | Authorize                | NPTA            |
      | Under Review             | NPTA            |
      | Outstanding Quote        | NPTA            |
      | To Be Declined           | NPTA            |
      | Bound Pending Data Entry | NPTA            |
      | Renewable - 6 Months     | NPTA            |
      | On Hold                  | PTA             |
      | Bound - Pending Actions  | PTA             |
      | In Progress              | PTA             |
      | Authorize                | PTA             |
      | Under Review             | PTA             |
      | Outstanding Quote        | PTA             |
      | To Be Declined           | PTA             |
      | Bound Pending Data Entry | PTA             |
      | Renewable - 6 Months     | PTA             |
      | On Hold                  | Actuary Manager |
      | Bound - Pending Actions  | Actuary Manager |
      | In Progress              | Actuary Manager |
      | Authorize                | Actuary Manager |
      | Under Review             | Actuary Manager |
      | Outstanding Quote        | Actuary Manager |
      | To Be Declined           | Actuary Manager |
      | Bound Pending Data Entry | Actuary Manager |
      | Renewable - 6 Months     | Actuary Manager |
      | On Hold                  | UW Manager      |
      | Bound - Pending Actions  | UW Manager      |
      | In Progress              | UW Manager      |
      | Authorize                | UW Manager      |
      | Under Review             | UW Manager      |
      | Outstanding Quote        | UW Manager      |
      | To Be Declined           | UW Manager      |
      | Bound Pending Data Entry | UW Manager      |
      | Renewable - 6 Months     | UW Manager      |
      | On Hold                  | Actuary         |
      | Bound - Pending Actions  | Actuary         |
      | In Progress              | Actuary         |
      | Authorize                | Actuary         |
      | Under Review             | Actuary         |
      | Outstanding Quote        | Actuary         |
      | To Be Declined           | Actuary         |
      | Bound Pending Data Entry | Actuary         |
      | Renewable - 6 Months     | Actuary         |
      | On Hold                  | All Access      |
      | Bound - Pending Actions  | All Access      |
      | In Progress              | All Access      |
      | Authorize                | All Access      |
      | Under Review             | All Access      |
      | Outstanding Quote        | All Access      |
      | To Be Declined           | All Access      |
      | Bound Pending Data Entry | All Access      |
      | Renewable - 6 Months     | All Access      |

  @api @regression @Sprint4 @Done
  Scenario Outline: Verify whether all deals fetched by get request matches with schema provided
    Given that I am submitting a get request for fetching all the <status> deals for the user having <user> access
    Then the get <status> status request response for the user having <user> access and the schema provided is matched successfully
    Examples:
      | status                   | user            |
      | On Hold                  | UW              |
      | Bound - Pending Actions  | UW              |
      | In Progress              | UW              |
      | Authorize                | UW              |
      | Under Review             | UW              |
      | Outstanding Quote        | UW              |
      | To Be Declined           | UW              |
      | Bound Pending Data Entry | UW              |
      | Renewable - 6 Months     | UW              |
      | On Hold                  | NPTA            |
      | Bound - Pending Actions  | NPTA            |
      | In Progress              | NPTA            |
      | Authorize                | NPTA            |
      | Under Review             | NPTA            |
      | Outstanding Quote        | NPTA            |
      | To Be Declined           | NPTA            |
      | Bound Pending Data Entry | NPTA            |
      | Renewable - 6 Months     | NPTA            |
      | On Hold                  | PTA             |
      | Bound - Pending Actions  | PTA             |
      | In Progress              | PTA             |
      | Authorize                | PTA             |
      | Under Review             | PTA             |
      | Outstanding Quote        | PTA             |
      | To Be Declined           | PTA             |
      | Bound Pending Data Entry | PTA             |
      | Renewable - 6 Months     | PTA             |
      | On Hold                  | Actuary Manager |
      | Bound - Pending Actions  | Actuary Manager |
      | In Progress              | Actuary Manager |
      | Authorize                | Actuary Manager |
      | Under Review             | Actuary Manager |
      | Outstanding Quote        | Actuary Manager |
      | To Be Declined           | Actuary Manager |
      | Bound Pending Data Entry | Actuary Manager |
      | Renewable - 6 Months     | Actuary Manager |
      | On Hold                  | UW Manager      |
      | Bound - Pending Actions  | UW Manager      |
      | In Progress              | UW Manager      |
      | Authorize                | UW Manager      |
      | Under Review             | UW Manager      |
      | Outstanding Quote        | UW Manager      |
      | To Be Declined           | UW Manager      |
      | Bound Pending Data Entry | UW Manager      |
      | Renewable - 6 Months     | UW Manager      |
      | On Hold                  | Actuary         |
      | Bound - Pending Actions  | Actuary         |
      | In Progress              | Actuary         |
      | Authorize                | Actuary         |
      | Under Review             | Actuary         |
      | Outstanding Quote        | Actuary         |
      | To Be Declined           | Actuary         |
      | Bound Pending Data Entry | Actuary         |
      | Renewable - 6 Months     | Actuary         |
      | On Hold                  | All Access      |
      | Bound - Pending Actions  | All Access      |
      | In Progress              | All Access      |
      | Authorize                | All Access      |
      | Under Review             | All Access      |
      | Outstanding Quote        | All Access      |
      | To Be Declined           | All Access      |
      | Bound Pending Data Entry | All Access      |
      | Renewable - 6 Months     | All Access      |
