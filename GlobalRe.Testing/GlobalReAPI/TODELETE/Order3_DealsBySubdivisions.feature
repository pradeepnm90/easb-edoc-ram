Feature: GRS Deals By Status and Sub-divisions API Feature
  This feature is to verify the Deals By Status and Sub-divisions API


@api @Sprint3
Scenario Outline: Verify whether I get a successful response returned for the deals by status and subdivisions request
Given that I am submitting a get request for fetching all the <status> deals with <subdivisions> subdivision
Then I receive a response with status as successful for the <status> status deal with <subdivisions> subdivision request
Examples:
  | status                   | subdivisions     |
  | On Hold                  | Casualty         |
  | Bound - Pending Actions  | Casualty Treaty  |
  | In Progress              | Cas Fac          |
  | Authorize                | Property         |
  | Under Review             | Intl Property    |
  | Outstanding Quote        | NA Property      |
  | To Be Declined           | Specialty        |
  | Bound Pending Data Entry | Specialty Non-PE |
  | Renewable - 6 Months     | Public Entity    |
#  | On Hold                  | All Team         |
#  | All                      | Casualty         |
#  | All                      | Casualty Treaty  |
#  | All                      | Cas Fac          |
#  | All                      | Property         |
#  | All                      | Intl Property    |
#  | All                      | NA Property      |
#  | All                      | Specialty        |
#  | All                      | Specialty Non-PE |
#  | All                      | Public Entity    |
#  | All                      | All Team         |


  @api @Sprint3
  Scenario Outline: Verify whether all deals fetched by get deals by status and subdivision request matches with schema provided
    Given that I am submitting a get request for fetching all the <status> deals with <subdivisions> subdivision
    Then the get <status> deals with <subdivisions> subdivision request response and the schema provided is matched successfully
    Examples:
      | status                   | subdivisions     |
      | On Hold                  | Casualty         |
      | Bound - Pending Actions  | Casualty Treaty  |
      | In Progress              | Cas Fac          |
      | Authorize                | Property         |
      | Under Review             | Intl Property    |
      | Outstanding Quote        | NA Property      |
      | To Be Declined           | Specialty        |
      | Bound Pending Data Entry | Specialty Non-PE |
      | Renewable - 6 Months     | Public Entity    |
#      | On Hold                  | All Team         |
#      | All                      | Casualty         |
#      | All                      | Casualty Treaty  |
#      | All                      | Cas Fac          |
#      | All                      | Property         |
#      | All                      | Intl Property    |
#      | All                      | NA Property      |
#      | All                      | Specialty        |
#      | All                      | Specialty Non-PE |
#      | All                      | Public Entity    |
#      | All                      | All Team         |

  @api
  Scenario Outline: Verify whether all deals fetched by get deals by status and subdivision request matches with db result
    Given that I am submitting a get request for fetching all the <status> deals with <subdivisions> subdivision
    And I fetch the list of deals having <status> status and <subdivisions> from DB
    Then the get deals by <status> status and <subdivisions> subdivision request response and the db query result is matched successfully
    Examples:
      |status                   |subdivisions|
      #|On Hold                  |Casualty|
      #|Bound - Pending Actions  |Casualty Treaty|
      |In Progress              |Cas Fac        |
      #|Authorize                |Property       |
      #|Under Review             |Intl Property  |
      #|Outstanding Quote        |NA Property    |
      #|To Be Declined           |Specialty      |
      #|Bound Pending Data Entry |Specialty Non-PE|
      #|Renewable - 6 Months     |Public Entity   |
      #|On Hold                  |All Team            |


   @api
   Scenario Outline: Verify the deal count of get deals by status and subdivisions request match the DB count
     Given that I am submitting a get request for fetching count of <status> deals with <subdivisions> subdivision
     And I fetch the count of deals having <status> status and <subdivisions> from DB
     Then I will be able to see the deal count of the request deals by <status> status and <subdivisions> match with DB count
     Examples:
       | status                   | subdivisions     |
       | On Hold                  | Casualty         |
#       | Bound - Pending Actions  | Casualty Treaty  |
#       | In Progress              | Cas Fac          |
#       | Authorize                | Property         |
#       | Under Review             | Intl Property    |
#       | Outstanding Quote        | NA Property      |
#       | To Be Declined           | Specialty        |
#       | Bound Pending Data Entry | Specialty Non-PE |
#       | Renewable - 6 Months     | Public Entity    |
#       | On Hold                  | All Team         |
