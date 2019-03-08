Feature: GRS Attachment Basis Lookup API Feature
  This feature is to verify theAttachment Basis Lookup API

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether I get a successful response returned for the Attachment Basis Lookup api call
    Given that I am submitting an Attachment Basis Lookup get request for fetching all the attachment basis types for type <assumedCededAllFlag>
    Then I receive a response with status as successful for the Attachment Basis Lookup request for <assumedCededAllFlag>
    Examples:
      | assumedCededAllFlag        |
      | Assumed     |
      | All         |

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether the Attachment Basis Lookup request response matches with DB
    Given that I am submitting an Attachment Basis Lookup get request for fetching all the attachment basis types for type <assumedCededAllFlag>
    Then the Attachment Basis Lookup get request response for <assumedCededAllFlag> and the db query result is matched successfully
    Examples:
      | assumedCededAllFlag    |
      | Assumed                |
      | All                    |

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether the Attachment Basis Lookup request response matches with the schema provided
    Given that I am submitting an Attachment Basis Lookup get request for fetching all the attachment basis types for type <assumedCededAllFlag>
    Then the Attachment Basis Lookup get request response for <assumedCededAllFlag> and the schema provided is matched successfully
    Examples:
      | assumedCededAllFlag    |
      | Assumed                |
      | All                    |