Feature: GRS Deal Summaries By Sub-divisions API Feature
  This feature is to verify the Deal Summaries By Sub-divisions API


  @api @Sprint3 @regression
  Scenario Outline: Verify whether I get a successful response returned for the deal summary by subdivision api call
    Given that I am submitting a get deal summary by <subdivisions> subdivision request
    Then I receive a response with status as successful for the deal summary by <subdivisions> subdivision request
  Examples:
    | subdivisions     |
    | Casualty         |
    | Casualty Treaty  |
    | Cas Fac          |
    | Property         |
    | Intl Property    |
    | NA Property      |
    | Specialty        |
    | Specialty Non-PE |
    | Public Entity    |
    | All Team         |

  @api @Sprint3 @regression
  Scenario Outline: Verify whether the SubDivisions request response matches with DB
    Given that I am submitting a get deal summary by <subdivisions> subdivision request
    Then the get deal summary by <subdivisions> subdivision request response and the db query result is matched successfully
    Examples:
      | subdivisions     |
      | Casualty         |
      | Casualty Treaty  |
      | Cas Fac          |
      | Property         |
      | Intl Property    |
      | NA Property      |
      | Specialty        |
      | Specialty Non-PE |
      | Public Entity    |
      | All Team         |


  @api @Sprint3 @regression
  Scenario Outline: Verify whether the deal summary by subdivision request response matches with the schema provided
    Given that I am submitting a get deal summary by <subdivisions> subdivision request
    Then the get deal summary by <subdivisions> subdivision request response and the schema provided is matched successfully
    Examples:
      | subdivisions     |
      | Casualty         |
      | Casualty Treaty  |
      | Cas Fac          |
      | Property         |
      | Intl Property    |
      | NA Property      |
      | Specialty        |
      | Specialty Non-PE |
      | Public Entity    |
      | All Team         |

