Feature: GRS Note Types API Feature
  This feature is to verify the GET request to fetch notetypes

  @api @Sprint9 @Done @regression @notes
  Scenario Outline: Verify whether I get a successful response for the fetch NoteTypes api call
    Given that I am submitting a NoteType API GET request for fetching all the notetypes for <assumedCededAllFlag>
    Then I verify if I get  response code as <expectedResponseCode>
    Examples:
      | assumedCededAllFlag    |expectedResponseCode|
      | assumed                |200                 |
      | all                    |200                 |
      | ceded                  |404                 |
      | A                      |404                 |
      | $                      |404                 |
      |                        |404                 |




  @api @Sprint9 @Done @regression @notes
  Scenario Outline: Verify whether the GET NoteTypes request response matches with DB
    Given that I am submitting a NoteType API GET request for fetching all the notetypes for <assumedCededAllFlag>
      Then Verify if the NoteTypes GET request response  for <assumedCededAllFlag> assumedCededAllFlag and the db query result is matched successfully
      Examples:
        | assumedCededAllFlag    |
        | assumed               |
        |all                    |

  @api @Sprint9 @Done @regression @notes
  Scenario Outline: Verify whether the GET NoteTypes request response matches with the schema provided
    Given that I am submitting a NoteType API GET request for fetching all the notetypes for <assumedCededAllFlag>
    Then The NoteTypes GET  request response for <assumedCededAllFlag> and the schema provided is matched successfully
    Examples:
      | assumedCededAllFlag    |
      | assumed               |
      |all                    |

