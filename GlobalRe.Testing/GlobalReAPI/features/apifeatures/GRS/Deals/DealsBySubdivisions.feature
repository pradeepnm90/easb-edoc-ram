Feature: GRS Deals By Status and Sub-divisions API Feature
  This feature is to verify the Deals By Status and Sub-divisions API


@api @Sprint3 @regression
Scenario Outline: Verify whether I get a successful response returned for the deals by status and subdivisions request
Given that I am submitting a get request for fetching all the <status> deals with <subdivisions> subdivision
Then I receive a response with status as successful for the <status> status deal with <subdivisions> subdivision request
Examples:
  | status                  | subdivisions                    |
  | On Hold                 | Casualty                        |
  | On Hold                 | Property                        |
  | On Hold                 | Specialty                       |
  | On Hold                 | Casualty*Property               |
  | On Hold                 | Casualty*Specialty              |
  | On Hold                 | Property*Specialty              |
  | On Hold                 | Casualty*Property*Specialty     |
  | Bound - Pending Actions | Casualty                        |
  | Bound - Pending Actions | Property                        |
  | Bound - Pending Actions | Specialty                       |
  | Bound - Pending Actions | Casualty*Property               |
  | Bound - Pending Actions | Casualty*Specialty              |
  | Bound - Pending Actions | Property*Specialty              |
  | Bound - Pending Actions | Casualty*Property*Specialty     |
  | In Progress             | Casualty                        |
  | In Progress             | Property                        |
  | In Progress             | Specialty                       |
  | In Progress             | Casualty*Property               |
  | In Progress             | Casualty*Specialty              |
  | In Progress             | Property*Specialty              |
  | In Progress             | Casualty*Property*Specialty     |
  | Renewable - 6 Months    | Casualty                        |
  | Renewable - 6 Months    | Property                        |
  | Renewable - 6 Months    | Specialty                       |
  | Renewable - 6 Months    | Casualty*Property               |
  | Renewable - 6 Months    | Casualty*Specialty              |
  | Renewable - 6 Months    | Property*Specialty              |
  | Renewable - 6 Months    | Casualty*Property*Specialty     |

  @api @Sprint3 @regression
  Scenario Outline: Verify whether all deals fetched by get deals by status and subdivision request matches with schema provided
    Given that I am submitting a get request for fetching all the <status> deals with <subdivisions> subdivision
    Then the get <status> deals with <subdivisions> subdivision request response and the schema provided is matched successfully
    Examples:
      | status                  | subdivisions                    |
      | On Hold                 | Casualty                        |
      | On Hold                 | Property                        |
      | On Hold                 | Specialty                       |
      | On Hold                 | Casualty*Property               |
      | On Hold                 | Casualty*Specialty              |
      | On Hold                 | Property*Specialty              |
      | On Hold                 | Casualty*Property*Specialty     |
      | Bound - Pending Actions | Casualty                        |
      | Bound - Pending Actions | Property                        |
      | Bound - Pending Actions | Specialty                       |
      | Bound - Pending Actions | Casualty*Property               |
      | Bound - Pending Actions | Casualty*Specialty              |
      | Bound - Pending Actions | Property*Specialty              |
      | Bound - Pending Actions | Casualty*Property*Specialty     |
      | In Progress             | Casualty                        |
      | In Progress             | Property                        |
      | In Progress             | Specialty                       |
      | In Progress             | Casualty*Property               |
      | In Progress             | Casualty*Specialty              |
      | In Progress             | Property*Specialty              |
      | In Progress             | Casualty*Property*Specialty     |
      | Renewable - 6 Months    | Casualty                        |
      | Renewable - 6 Months    | Property                        |
      | Renewable - 6 Months    | Specialty                       |
      | Renewable - 6 Months    | Casualty*Property               |
      | Renewable - 6 Months    | Casualty*Specialty              |
      | Renewable - 6 Months    | Property*Specialty              |
      | Renewable - 6 Months    | Casualty*Property*Specialty     |

  @api @regression @Testing
  Scenario Outline: Verify whether all deals fetched by get deals by status and subdivision request matches with db result
    Given that I am submitting a get request for fetching all the <status> deals with <subdivisions> subdivision
    And I fetch the list of deals having <status> status and <subdivisions> from DB
    Then the get deals by <status> status and <subdivisions> subdivision request response and the db query result is matched successfully
    Examples:
      | status                  | subdivisions                    |
      | On Hold                 | Casualty                        |
      | On Hold                 | Property                        |
      | On Hold                 | Specialty                       |
      | On Hold                 | Casualty*Property               |
      | On Hold                 | Casualty*Specialty              |
      | On Hold                 | Property*Specialty              |
      | On Hold                 | Casualty*Property*Specialty     |
      | Bound - Pending Actions | Casualty                        |
      | Bound - Pending Actions | Property                        |
      | Bound - Pending Actions | Specialty                       |
      | Bound - Pending Actions | Casualty*Property               |
      | Bound - Pending Actions | Casualty*Specialty              |
      | Bound - Pending Actions | Property*Specialty              |
      | Bound - Pending Actions | Casualty*Property*Specialty     |
      | In Progress             | Casualty                        |
      | In Progress             | Property                        |
      | In Progress             | Specialty                       |
      | In Progress             | Casualty*Property               |
      | In Progress             | Casualty*Specialty              |
      | In Progress             | Property*Specialty              |
      | In Progress             | Casualty*Property*Specialty     |
      | Renewable - 6 Months    | Casualty                        |
      | Renewable - 6 Months    | Property                        |
      | Renewable - 6 Months    | Specialty                       |
      | Renewable - 6 Months    | Casualty*Property               |
      | Renewable - 6 Months    | Casualty*Specialty              |
      | Renewable - 6 Months    | Property*Specialty              |
      | Renewable - 6 Months    | Casualty*Property*Specialty     |



   @api @regression
   Scenario Outline: Verify the deal count of get deals by status and subdivisions request match the DB count
     Given that I am submitting a get request for fetching count of <status> deals with <subdivisions> subdivision
     And I fetch the count of deals having <status> status and <subdivisions> from DB
     Then I will be able to see the deal count of the request deals by <status> status and <subdivisions> match with DB count
     Examples:
       | status                  | subdivisions                    |
       | On Hold                 | Casualty                        |
       | On Hold                 | Property                        |
       | On Hold                 | Specialty                       |
       | On Hold                 | Casualty*Property               |
       | On Hold                 | Casualty*Specialty              |
       | On Hold                 | Property*Specialty              |
       | On Hold                 | Casualty*Property*Specialty     |
       | Bound - Pending Actions | Casualty                        |
       | Bound - Pending Actions | Property                        |
       | Bound - Pending Actions | Specialty                       |
       | Bound - Pending Actions | Casualty*Property               |
       | Bound - Pending Actions | Casualty*Specialty              |
       | Bound - Pending Actions | Property*Specialty              |
       | Bound - Pending Actions | Casualty*Property*Specialty     |
       | In Progress             | Casualty                        |
       | In Progress             | Property                        |
       | In Progress             | Specialty                       |
       | In Progress             | Casualty*Property               |
       | In Progress             | Casualty*Specialty              |
       | In Progress             | Property*Specialty              |
       | In Progress             | Casualty*Property*Specialty     |
       | Renewable - 6 Months    | Casualty                        |
       | Renewable - 6 Months    | Property                        |
       | Renewable - 6 Months    | Specialty                       |
       | Renewable - 6 Months    | Casualty*Property               |
       | Renewable - 6 Months    | Casualty*Specialty              |
       | Renewable - 6 Months    | Property*Specialty              |
       | Renewable - 6 Months    | Casualty*Property*Specialty     |

