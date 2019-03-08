Feature: GRS CoverageBasisLookup API Feature
  This feature is to verify the CoverageBasis Lookup API

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether I get a successful response returned for the CoverageBasis Lookup api call
    Given that I am submitting a CoverageBasis Lookup GET request for fetching all the coveragebasis with the <assumedCededAllFlag> flag
    Then I receive a response with status as successful for the CoverageBasis Lookup request for <assumedCededAllFlag> flag
    Examples:
      | assumedCededAllFlag|
      | Assumed            |
      | All                |


  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether the Coverage Basis Lookup request response matches with DB
    Given that I am submitting a CoverageBasis Lookup GET request for fetching all the coveragebasis with the <assumedCededAllFlag> flag
    Then the CoverageBasis Lookup GET request response for <assumedCededAllFlag> flag and the db query result is matched successfully
    Examples:
      | assumedCededAllFlag    |
      | Assumed                |
      | All                    |

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether the CoverageBasis Lookup request response matches with the schema provided
    Given that I am submitting a CoverageBasis Lookup GET request for fetching all the coveragebasis with the <assumedCededAllFlag> flag
    Then the CoverageBasis Lookup get request response for <assumedCededAllFlag> flag and the schema provided is matched successfully
    Examples:
      | assumedCededAllFlag    |
      | Assumed                |
      | All                    |
