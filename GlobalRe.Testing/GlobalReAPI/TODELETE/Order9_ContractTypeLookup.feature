Feature: GRS ContractTypeLookup API Feature
  This feature is to verify the ContractType Lookup API

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether I get a successful response returned for the ContractType Lookup api call
    Given that I am submitting a ContractType Lookup GET request for fetching all the ContractTypes for the <assumedCededFlag> flag
    Then I receive a response with status as successful for the ContractType Lookup request for <assumedCededFlag> flag
    Examples:
      | assumedCededFlag|
      | Assumed         |


  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether the Contract Type Lookup request response matches with DB
    Given that I am submitting a ContractType Lookup GET request for fetching all the ContractTypes for the <assumedCededFlag> flag
    Then the ContractType Lookup GET request response for <assumedCededFlag> flag and the db query result is matched successfully
    Examples:
      | assumedCededFlag    |
      | Assumed             |

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether the ContractType Lookup request response matches with the schema provided
    Given that I am submitting a ContractType Lookup GET request for fetching all the ContractTypes for the <assumedCededFlag> flag
    Then the ContractType Lookup get request response for <assumedCededFlag> flag and the schema provided is matched successfully
    Examples:
      | assumedCededFlag    |
      | Assumed             |
