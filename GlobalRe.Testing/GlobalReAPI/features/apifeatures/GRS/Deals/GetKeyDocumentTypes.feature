Feature: GRS Key Document Types GET API Feature
  This feature is to verify the key document Types GET API

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether I get a successful response returned for the Key Document Types GET api call
    Given that I am submitting an Key Document Types GET api request for fetching all the key docs types for deal <dealNumber> with documents flag as <getDocTypes>
    Then I receive a response with status as successful for the Key Document Types API request for <getDocTypes>
    Examples:
      | dealNumber  | getDocTypes        |
      | 1383315     | false              |

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether the Attachment Basis Lookup request response matches with DB
    Given that I am submitting an Attachment Basis Lookup get request for fetching all the attachment basis types for type <assumedCededAllFlag>
    Then the Attachment Basis Lookup get request response for <assumedCededAllFlag> and the db query result is matched successfully
    Examples:
      | assumedCededAllFlag    |
      | Assumed                |
      | All                    |

  @api @Sprint6 @Done @regression
  Scenario Outline: Verify whether the Key Document Types API request response matches with the schema provided
    Given that I am submitting an Key Document Types GET api request for fetching all the key docs types for deal <dealNumber> with documents flag as <getDocTypes>
    Then the Key Document Types GET API request response and the schema provided is matched successfully
    Examples:
      | dealNumber  | getDocTypes        |
      | 1383315     | false              |