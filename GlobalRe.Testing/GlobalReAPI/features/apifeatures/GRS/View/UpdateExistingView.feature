Feature: Views - Update Existing Views
  This feature is to verify the Put - View and to validate if the existing view is updated sucessfully and validate the same in the data base


  @api @Sprint10 @regression
  Scenario Outline: Verify whether I get a successful response returned for the PUT view API call and the data is successfully submitted to DB
    Given that I am submitting a PUT view API request by passing viewID <viewId> for the field <field> with the value <value> to the <actualValue> actual values
    And I receive a response with status as successful for the PUT view API request
    And I am able to find the data of the field <field> in the DB successfully updated the view with viewID <viewId> with the new value <value> for the view with <actualValue> actual values
    Then reset the data for the future test with the <actualValue> actual value
    Examples:
      | viewId  | field    | value                 | actualValue                                                   |
      | 11      | layout   | Test New DP           | 11*3627*SubmissionWorkbench*From API 3*false*Test New DP      |
      | 11      | default  | false                 | 11*3627*SubmissionWorkbench*From API 3*false*Test New DP      |
      | 6       | layout   | Testing the layout    | 6*3627*SubmissionWorkbench*From API 1*false*Test New DP       |
      | 6       | default  | true                  | 6*3627*SubmissionWorkbench*From API 1*false*Test New DP       |

  @Sprint10 @regression
  Scenario Outline: Verify whether PUT view API response matches successfully with the schema provided
    Given that I am submitting a PUT view API request by passing viewID <viewId> for the field <field> with the value <value> to the <actualValue> actual values
    And I receive a response with status as successful for the PUT view API request
    And I receive a PUT view api call By <viewId> viewId request response for the <field> with value <value> which matches with the schema provided
    Then reset the data for the future test with the <actualValue> actual value
    Examples:
      | viewId  | field    | value                 | actualValue                                                    |
      | 11      | layout   | Test New DP           | 11*3627*SubmissionWorkbench*From API 3*false*Test New DP       |


