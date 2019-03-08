Feature: GRS Teams options and sub options Feature
  This feature covers the scenarios identified to verify the GRS Teams with options and sub options

  @Sprint3 @regression
  Scenario Outline: Verify the team options and sub options under Team field
    Given I open the browser and navigate to GRS link
    And I see the Team field on the GRS Home page
    And I click on the Team field
    And I will be abe to see <teams> team with <sub1> and <sub2> sub options
    And I click to select the <teams> team option
    Then I will be able to see <sub1> and <sub2> sub-options getting selected by default on selecting the team option
    Examples:
      | teams    | sub1            | sub2    |
      | Casualty | Casualty Treaty | Cas Fac |
      #|Property|Intl Property  |NA Property|
      #|Specialty|Specialty Non-PE  |Public Entity|

  @Sprint3 @regression
  Scenario Outline: Verify unselecting of sub options under the team options
    Given I open the browser and navigate to GRS link
    And I click on the Team field
    And I click to select the <teams> team option
    And I click to unselect the <sub1> sub option
    Then I will be able to see <sub1> sub option and <teams> gets unselected
    Examples:
      | teams    | sub1            | sub2    |
      | Casualty | Casualty Treaty | Cas Fac |
      #|Property|Intl Property  |NA Property|
      #|Specialty|Specialty Non-PE  |Public Entity|

  @Sprint3 @regression
  Scenario Outline: Verify the counts and deals displayed on selecting a team
    Given I open the browser and navigate to GRS link
    And I click on the Team field
    And I click to select the <teams> team option
    Then I will be able to see the count of deals on <panels> as per the selected <teams> team

    Examples:
      | panels                  | teams     |
      | In Progress             | Casualty  |
      | In Progress             | Property  |
      | In Progress             | Specialty |
      | On Hold                 | Casualty  |
      | On Hold                 | Property  |
      | On Hold                 | Specialty |
      | Renewable - 6 Months    | Casualty  |
      | Renewable - 6 Months    | Property  |
      | Renewable - 6 Months    | Specialty |
      | Bound - Pending Actions | Casualty  |
      | Bound - Pending Actions | Property  |
      | Bound - Pending Actions | Specialty |
  #|In Progress|Casualty Treaty|

 # |teams   |panels|
  #|Casualty|In Progress|
  #|Casualty Treaty|On Hold|
  #|Cas Fac|Bound - Pending Actions|
  #|Property|Renewable - 6 Months  |
  #|Intl Property  |
  #|NA Property|
  #|Specialty|
  #|Specialty Non-PE  |
  #|Public Entity|

  @Sprint3 @regression
  Scenario Outline: verify the selected team options and sub-options search criteria persists through out the session
    Given I open the browser and navigate to GRS link
    And I click on the Team field
    And I click to select the <teams> team option
    And I click on the panel
    And I click on the Team field
    And I will be able to see the previously selected <teams> team options search criteria persist
    Examples:
      | teams           |
      | Casualty        |
#      | Casualty Treaty |
#      | Cas Fac         |
      #|Property|Intl Property  |NA Property|
      #|Specialty|Specialty Non-PE  |Public Entity|