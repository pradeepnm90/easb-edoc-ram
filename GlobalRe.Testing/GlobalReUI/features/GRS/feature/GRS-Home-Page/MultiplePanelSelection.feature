Feature: GRS Home Page Feature
  @regression
  Scenario Outline: Verify whether I see the list of In-Progress statuses when I click on the In-Progress panel and multi panel selection
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    Then the panel will display the list of <substatus> wordings when the <status> Panel is clicked
    Examples:
      | status                                                     | substatus                                                                                    |
      | In Progress                                                | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
      | In Progress,On Hold                                        | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
      | On Hold,In Progress                                        | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
      | In Progress,Bound - Pending Actions                        | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
      | Bound - Pending Actions,In Progress                        | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
      | In Progress,On Hold,Bound - Pending Actions                | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
      | Bound - Pending Actions,On Hold,In Progress                | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
      | Renewable - 6 Months,In Progress                           | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |



  @regression
  Scenario Outline: Verify whether the list of In-Progress statuses are not displayed when I click on the Bound - Pending Actions On Hold Renewable - 6 Months panels and multi panel selection other than In progress panel
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    Then the panel will not display the list of <substatus> wordings when the <status> Panel is clicked
    Examples:
    | status                                | substatus                                                                                    |
    | On Hold                               | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
    | Bound - Pending Actions               | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
    | Renewable - 6 Months                  | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
    | On Hold,Bound - Pending Actions       | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
    | Bound - Pending Actions,On Hold       | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
    | On Hold,Renewable - 6 Months          | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |
    | Renewable - 6 Months,On Hold          | Under Review*Authorize*Outstanding Quote*To Be Declined*Bound Pending Data Entry             |