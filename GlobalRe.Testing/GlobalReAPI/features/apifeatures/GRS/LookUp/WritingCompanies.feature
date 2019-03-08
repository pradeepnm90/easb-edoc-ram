Feature: GRS ContractTypeLookup API Feature
  This feature is to verify the ContractType Lookup API

  @api @Sprint7 @Done @regression
  Scenario Outline: Verify whether I get a successful response returned for the WritingCompanies api call
    Given that I am submitting a Writingcompanies  GET request for fetching all the WritingCompanies for the <flag> flag
    Then I receive a response with status as successful for theWriting Companies API request for <flag> flag
    Examples:
      | flag         |
      | TRUE         |
      |              |


  @api @Sprint7 @Done @regression
  Scenario Outline: Verify whether the Contract Type Lookup request response matches with DB
    Given that I am submitting a Writingcompanies  GET request for fetching all the WritingCompanies for the <flag> flag
    Then the Writingcompanies  GET request response for <flag> flag and the db query result is matched successfully
    Examples:
      | flag    |
      | TRUE    |
#      |         |

  @api @Sprint7 @Done @regression
  Scenario Outline: Verify whether the ContractType Lookup request response matches with the schema provided
    Given that I am submitting a Writingcompanies  GET request for fetching all the WritingCompanies for the <flag> flag
    Then the Writingcompanies  get request response for <flag> flag and the schema provided is matched successfully
    Examples:
      | flag    |
      | TRUE    |
      |         |




