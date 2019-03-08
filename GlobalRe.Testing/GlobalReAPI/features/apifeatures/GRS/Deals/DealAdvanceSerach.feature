Feature: GRS Deal Advance Search feature API
  This feature is to validate deal advance search includes Subdivision, Product Line (PL2), Exposure Group, Exposure Name in Deal Filtering within Workbench
#
#  @api @Sprint9 @Done @regression
#  Scenario Outline: Verify whether I get a proper search results for all different combinations of filters and validated with the Database
##    Given that I am submitting a Put Deal api call By <dealnumber> Deal Number for the field <field> with the value <value> for the deal with <status> status with actual values <actualvalue>
##    And I receive a response with proper cedant information warning message for the <status> deal for the Put Deal By Deal Number api request with expected warning message <expectedWarningMessage>
#    Given validation of the data '<subdivision>' and '<Productline>' '<exposuregroup>'
#    Examples:
#      | subdivision1 | Productline     | exposuregroup           | exposuretype        |
#      | Casualty    | Auto Reins      | CA - Auto - Commercial  | PI-ArcRetro-Lat Am  |




  @api @Sprint9 @Done @regression
  Scenario Outline: Verify whether I get a successful response returned for the GET Deal Advance search call
    Given that I am submitting a GET Deal Advance search api call By passing <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode> filters
    Then I receive a response with status as successful for the GET Deal Advance search API request
    Examples:
      | statusCode | subdivision           | productLine         | exposureGroup                                                                      | exposureType    |
      | 3,16        | Casualty*Specialty    | Agriculture_Reins  | NA                                                                                 | NA             |
      | NA          | Casualty              | Cas_Fac_(GRe)      | CA_-_Cas_Fac_-_Auto_Liability*CA_-_Cas_Fac_-_Comprehensive*CA_-_Cas_Fac_-_Umbrella | CA_-_Cas_Fac_-_Auto_Liability*CA_-_Cas_Fac_-_Comprehensive*CA_-_Cas_Fac_-_Umbrella|
      | 3           | Casualty              | Na                 | NA                                                                                 | NA                                                                                   |



  @api @Sprint9 @Done @regression
  Scenario Outline: Verify whether I get a proper search results for all different combinations of filters and validated with the Database
    Given that I am submitting a GET Deal Advance search api call By passing <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode> filters
    And I receive a response with deal numbers associated with the filter <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode>
    Then The GET deal Advance search <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode> API response  with the Data base is matched successfully

#    Given validation data <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode>
    Examples:
      | statusCode | subdivision           | productLine                                    | exposureGroup               | exposureType                              |
      | 3,16       | Casualty    | Auto_Reins*Cas_Fac_(GRe)                       | NA                          | NA                                        |
      | 3          | Casualty              | NA                                             | NA                          | NA                                        |
      | NA         | Casualty              | NA                                             | NA                          | NA                                        |
      | NA         | Specialty             | NA                                             | NA                          | NA                                        |
      | NA         | Casualty              | Medical_Malpractice_Reins*Whole_Account_Reins  | NA                          | NA                                        |
      | NA         | Specialty             | Agriculture_Reins                              | SP_-_Agri                   | SP_-_Agri_-_Other                         |
      | 3          | Specialty             | Public_Entity_(GRe)                            | SP_-_Public_Entity          | SP_-_Public_Entity_-_Community_Colleges   |



  @api @Sprint9 @Done @regression
  Scenario Outline: Verify whether GET Deal Advance search API response matches successfully with the schema provided
    Given that I am submitting a GET Deal Advance search api call By passing <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode> filters
    And I receive a response with deal numbers associated with the filter <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode>
    Then The GET deal Advance search <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode> API response  with the schema provided is matched successfully

#    Given validation data <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode>
    Examples:
      | statusCode | subdivision           | productLine               | exposureGroup                     | exposureType                       |
      | 3,16       | Casualty              | Auto_Reins*Cas_Fac_(GRe)  | NA                                | NA                                 |
      | 3          | Casualty              | NA                        | NA                                | NA                                 |
      | NA         | Casualty              | NA                        | NA                                | NA                                 |
      | NA         | Casualty              | Medical_Malpractice_Reins | NA                                | NA                                 |
      | NA         | Property              | NA                        | NA                                | NA                                 |
      | NA         | Property              | Property_Reins            | PN_-_Reins_Prop_North_America_CAT | NA                                 |


  @api @Sprint9 @Done @regression
  Scenario Outline: Verify whether I get proper warning message for invalid parameters in GET Deal Advance search API response
    Given that I am submitting a GET Deal Advance search api call By  invalid parameters <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode> filters
    Then I receive a response with warning message associated with the filter <subdivision> <productLine> <exposureGroup> <exposureType> <statusCode> and matches with the <expectedWarningMessage>

    Examples:
      | statusCode | subdivision   | productLine  | exposureGroup    | exposureType  | expectedWarningMessage                                                           |
      | 3          | 13            | NA           | NA               | NA            | SubDivision '13' is not valid.                                                   |
      | 13         | 13            | NA           | NA               | NA            | Status code '13' is not valid.                                                   |
      | 3          | 3             | 100          | NA               | NA            | Productline '100' is not valid or does not have valid higher level parameters.   |
      | 3          | 3             | 308          | 100              | NA            | ExposureGroup '100' is not valid or does not have valid higher level parameters. |
      | 3          | 3             | 308          | 77               | 100           | Exposuretype '100' is not valid or does not have valid higher level parameters.  |
