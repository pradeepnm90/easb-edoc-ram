Feature: GRS Home Page Feature
  This feature covers the scenarios identified to verify the GRS Home Page

  @sprint1 @GRS-4 @regression
  Scenario: Verify whether user gets navigated to GRS Home Page without login in
    Given I open the browser and navigate to GRS link
    Then I see the GRS Homepage with the users userid

  @Sprint4 @Done @regression
  Scenario: Verify whether user sees first name and last name when logged in to GRS
    Given I open the browser and navigate to GRS link
    Then I see the GRS Homepage with the users first name and last name

  @regression
  Scenario: Verify the link to QlikView
    Given I open the browser and navigate to GRS link
    Then I see the GRS Homepage with the users userid
    And I see the QuickLinks link and Clicked
    And I see the QlikView link
    And I click on the QlikView link
    Then I will be able to see in Global Re Dashboard launched in new browser window

  @regression
  Scenario: Verify the link to ERMS when Citrix and ERMS are logged in only once per a day
    Given I open the browser and navigate to GRS link
    Then I see the GRS Homepage with the users userid
    And I see the ERMS link
    And I click on the ERMS link
    Then I will be able to see the ERMS application launched

  #Scenario: Verify the link to ERMS when Citrix and ERMS are not logged in even once per a day
  #  Given I open the browser and navigate to GRS link
  #  Then I see the GRS Homepage with the users userid
  #  And I see the ERMS link
  #  And I click on the ERMS link
  #  Then i will not be able to see the ERMS application launched

  @sprint1 @GRS-21 @regression
  Scenario Outline: Verify whether I see the panel on GRS Homepage with the needed status wording
    Given I open the browser and navigate to GRS link
    Then a panel is displayed with the wording <status>
    Examples:
      | status                  |
      | In Progress             |
#      | Renewable - 6 Months    |
#      | On Hold                 |
#      | Bound - Pending Actions |

  @sprint1 @GRS-20 @Sprint3 @Done @Sprint4 @regression
  Scenario Outline: Verify whether I see the panel on GRS Homepage with the needed wording and the count of Deals
    Given I open the browser and navigate as <user> user to GRS link
    Then a panel is displayed with the needed wording <status> with the count of the Deals of the <user> user
    Examples:
      | status                  | user            |
      | In Progress             | All Access      |
      | Renewable - 6 Months    | All Access      |
      | On Hold                 | All Access      |
      | Bound - Pending Actions | All Access      |
      | In Progress             | UW              |
      | Renewable - 6 Months    | UW              |
      | On Hold                 | UW              |
      | Bound - Pending Actions | UW              |
      | In Progress             | NPTA            |
      | Renewable - 6 Months    | NPTA            |
      | On Hold                 | NPTA            |
      | Bound - Pending Actions | NPTA            |
      | In Progress             | Actuary         |
      | Renewable - 6 Months    | Actuary         |
      | On Hold                 | Actuary         |
      | Bound - Pending Actions | Actuary         |
      | In Progress             | PTA             |
      | Renewable - 6 Months    | PTA             |
      | On Hold                 | PTA             |
      | Bound - Pending Actions | PTA             |
      | In Progress             | Actuary Manager |
      | Renewable - 6 Months    | Actuary Manager |
      | On Hold                 | Actuary Manager |
      | Bound - Pending Actions | Actuary Manager |
      | In Progress             | UW Manager      |
      | Renewable - 6 Months    | UW Manager      |
      | On Hold                 | UW Manager      |
      | Bound - Pending Actions | UW Manager      |


  @sprint1 @GRS-34 @Sprint3 @Done @Sprint4 @regression
  Scenario Outline: Verify whether the user sees the needed status deals when the corresponding status panel is clicked
    Given I open the browser and navigate as <user> user to GRS link and click on the <status> Panel
    And the deals of <status> for the <user> user are displayed in the Grid below in the descending order
    Then the <status> status deals details displayed has the needed set of fields for the specific <user> user
    Examples:
      | status                  | user            |
      | In Progress             | All Access      |
      | Renewable - 6 Months    | All Access      |
      | On Hold                 | All Access      |
      | Bound - Pending Actions | All Access      |
#      | In Progress             | UW              |
#      | Renewable - 6 Months    | UW              |
#      | On Hold                 | UW              |
#      | Bound - Pending Actions | UW              |
#      | In Progress             | NPTA            |
#      | Renewable - 6 Months    | NPTA            |
#      | On Hold                 | NPTA            |
#      | Bound - Pending Actions | NPTA            |
      | In Progress             | Actuary         |
      | Renewable - 6 Months    | Actuary         |
      | On Hold                 | Actuary         |
      | Bound - Pending Actions | Actuary         |
      | In Progress             | PTA             |
      | Renewable - 6 Months    | PTA             |
      | On Hold                 | PTA             |
      | Bound - Pending Actions | PTA             |
      | In Progress             | Actuary Manager |
      | Renewable - 6 Months    | Actuary Manager |
      | On Hold                 | Actuary Manager |
      | Bound - Pending Actions | Actuary Manager |
      | In Progress             | UW Manager      |
      | Renewable - 6 Months    | UW Manager      |
      | On Hold                 | UW Manager      |
      | Bound - Pending Actions | UW Manager      |

  @sprint1 @GRS-10 @regression
  Scenario Outline: Verify the sort functionality of the needed status grid
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I see the grid displayed below with the deals having <status> status
    And I click on a column label
    And I will be able to see the data sorted ascending
    And I click on a column label
    Then I will be able to see the data sorted descending
    Examples:
      | status                  |
      | In Progress             |
      #| Renewable - 6 Months    |
      #| On Hold                 |
      #| Bound - Pending Actions |


  @sprint1 @GRS-12 @regression
  Scenario Outline: Verify the reorder functionality of the needed status grid
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I select a label of the column on the left and drag the column to right and I should see the column moved to the right of the screen
    Then I select a label of the column on the right and drag the column to left I should see the column moved to the left of the screen
    Examples:
      | status                  |
      | In Progress             |
      #| Renewable - 6 Months    |
      #| On Hold                 |
      #| Bound - Pending Actions |

  @regression
  Scenario Outline: Verify whether I see the list of In-Progress statuses when I click on the In-Progress panel
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    Then the panel will display the list of <substatus> wordings when the <status> Panel is clicked
    Examples:
      | status      | substatus                |
      | In Progress | Under Review             |
      | In Progress | Authorize                |
      | In Progress | Outstanding Quote        |
      | In Progress | To Be Declined           |
      | In Progress | Bound Pending Data Entry |

  @regression
  Scenario Outline: Verify whether I see the count of deals against each sub-status in the In-Progress panel
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    Then the panel will display the list of <substatus> wordings with the count of <substatus> substatus deals
    Examples:
      | status      | substatus                |
      | In Progress | Under Review             |
      | In Progress | Authorize                |
      | In Progress | Outstanding Quote        |
      | In Progress | To Be Declined           |
      | In Progress | Bound Pending Data Entry |

  @regression
  Scenario Outline: Verify whether all statuses with deal count greater than zero  are selected by default
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    Then the In Progress panel will display the <substatus> panel if count greater than zero as enabled and selected by default
    Examples:
      | status      | substatus                |
      | In Progress | Under Review             |
      | In Progress | Authorize                |
      | In Progress | Outstanding Quote        |
      | In Progress | To Be Declined           |
      | In Progress | Bound Pending Data Entry |

  @regression
  Scenario Outline: Verify whether the count of deals on the corresponding status panel changes if the user unselects all other substatus apart from corresponding substatus
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I uncheck all other substatus apart from <substatus> substatus
    Then the total count on the <status> panel should be updated with the number of <substatus> deals
    Examples:
      | status      | substatus                |
      | In Progress | Under Review             |
      | In Progress | Authorize                |
      | In Progress | Outstanding Quote        |
      | In Progress | To Be Declined           |
      | In Progress | Bound Pending Data Entry |

  @regression
  Scenario Outline:  Verify whether the deals displayed in grid are updated to show only corresponding substatus deals if the user unselects all other substatus apart from corresponding substatus
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I uncheck all other substatus apart from <substatus> substatus
    Then the grid displaying the deals should now only show <substatus> deals
    Examples:
      | status      | substatus                |
      | In Progress | Under Review             |
      | In Progress | Authorize                |
      | In Progress | Outstanding Quote        |
      | In Progress | To Be Declined           |
      | In Progress | Bound Pending Data Entry |

  @Sprint3 @Done @Sprint4 @regression
  Scenario Outline: Verify whether the count of deals on the main status panel changes if the user unselects all other statuses apart from two substatus selected
    Given I open the browser and navigate as <user> user to GRS link and click on the <status> Panel
    And I uncheck all other statuses apart from <substatus1> and <substatus2> substatus
    Then the total count on the <status> status panel should be updated with the count of <substatus1> and <substatus2> substatus deals for the <user> user
    Examples:
      | status      | substatus1   | substatus2 | user            |
      | In Progress | Under Review | Authorize  | All Access      |
      | In Progress | Under Review | Authorize  | UW              |
      | In Progress | Under Review | Authorize  | NPTA            |
      | In Progress | Under Review | Authorize  | Actuary         |
      | In Progress | Under Review | Authorize  | PTA             |
      | In Progress | Under Review | Authorize  | Actuary Manager |
      | In Progress | Under Review | Authorize  | UW Manager      |
#      | In Progress | Authorize                 |Outstanding Quote          |
#      | In Progress | Outstanding Quote         |To Be Declined             |
#      | In Progress | To Be Declined            |Bound Pending Data Entry   |


  @Sprint3 @Done @Sprint4 @regression
  Scenario Outline: Verify whether the deals displayed in grid are updated to show only deals of selected two substatus if the user unselects all other substatus apart from the two substatus
    Given I open the browser and navigate as <user> user to GRS link and click on the <status> Panel
    And I uncheck all other statuses apart from <substatus1> and <substatus2> substatus
    Then the grid displaying the deals should now only show specific deals of <substatus1> and <substatus2> substatus of the <user> user when the <status> status panel is selected
    Examples:
      | status      | substatus1   | substatus2 | user            |
      | In Progress | Under Review | Authorize  | All Access      |
#      | In Progress | Under Review | Authorize  | UW              |
#      | In Progress | Under Review | Authorize  | NPTA            |
      | In Progress | Under Review | Authorize  | Actuary         |
      | In Progress | Under Review | Authorize  | PTA             |
      | In Progress | Under Review | Authorize  | Actuary Manager |
      | In Progress | Under Review | Authorize  | UW Manager      |

  @regression
  Scenario Outline: Verify whether a sub-status is disabled if the count is zero
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And the needed <substatus> substatus are displayed when the <status> panel is clicked
    Then I see a <substatus> as disabled if the count is zero
    Examples:
      | status               | substatus                                                                        |
      | In Progress          | Under Review,Authorize,Outstanding Quote,To Be Declined,Bound Pending Data Entry |
      | DUMMY - DEV USE Only | Inquired,In Due Diligence,Informational Agreement                                |

  @regression
  Scenario Outline: Verify whether a sub-status is enabled and selected by default if the count is greater than zero
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And the needed <substatus> substatus are displayed when the <status> panel is clicked
    Then the <substatus> with count greater than zero should be enabled and selected by default
    Examples:
      | status      | substatus                                                                        |
      | In Progress | Under Review,Authorize,Outstanding Quote,To Be Declined,Bound Pending Data Entry |

  @regression
  Scenario Outline: Verify the Remove Columns functionality within the grid using column Tool menu
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the Tool menu in the column heading
#    And I will be able to see tool menu popup
    And I click on the columns tab within the Tool menu popup
    And I will be able to see grid column names with checkboxes
    #And I will be able to see column checkboxes are checked for columns displayed in the grid
    #And I will be able to see column checkboxes are unchecked for columns not displayed in the grid
    And I click to uncheck the checkbox of the <columnname> column not to be displayed in the grid
    Then I will be able to see the grid not displaying the <columnname> column
    Examples:
      | status                  |columnname       |
      | In Progress             |Submitted        |
      #| Renewable - 6 Months    |Contract         |
      #| On Hold                 |Contract         |
      #| Bound - Pending Actions |Contract         |

  @regression
  Scenario Outline: Verify the Remove Columns functionality within the grid using Tool Panel
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the Tool menu in the column heading
    And I will be able to see tool menu popup
    And I click to select the Tool Panel option
    And I will be able to see Tool Panel displayed on the right side of the grid
    And I click to uncheck the checkbox of the <columnname> column not to be displayed in the grid
    Then I will be able to see the grid not displaying the <columnname> column
    Examples:
      | status                  |columnname       |
      | In Progress             |Submitted         |
      #| Renewable - 6 Months    |Contract         |
      #| On Hold                 |Contract         |
      #| Bound - Pending Actions |Contract         |

  @regression
  Scenario Outline: Verify the Remove Column functionality within the In Progress grid by dragging out the column from the grid
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I drag the <columnname> column out of the grid
    Then removed column <columnname> is not displayed in the grid
    Examples:
      | status                  |columnname       |
      | In Progress             |Submitted        |
      #| Renewable - 6 Months    |Contract         |
      #| On Hold                 |Contract         |
      #| Bound - Pending Actions |Contract         |

  @regression
  Scenario Outline: Verify the Add Columns functionality within the grid using column Tool menu
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the Tool menu in the column heading
    And I will be able to see tool menu popup
    And I click to select the Tool Panel option
    And I will be able to see Tool Panel displayed on the right side of the grid
    And I will be able to see column checkboxes are checked for columns displayed in the grid
    And I will be able to see column checkboxes are unchecked for columns not displayed in the grid
    And I select the checkbox to check the <columnname> column to be displayed in the grid
    Then I will be able to see the grid displaying the <columnname> column
    Examples:
      | status                  |columnname       |
      | In Progress             |Expiration       |
      #| Renewable - 6 Months    |Contract         |
      #| On Hold                 |Contract         |
      #| Bound - Pending Actions |Contract         |

  @regression
  Scenario Outline: Verify the Add Columns functionality within the grid using Tool Panel
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the Tool menu in the column heading
    And I will be able to see tool menu popup
    And I click to select the Tool Panel option
    And I will be able to see Tool Panel displayed on the right side of the grid
    And I select the checkbox to check the <columnname> column to be displayed in the grid
    Then I will be able to see the grid displaying the <columnname> column
    Examples:
      | status                  |columnname       |
      | In Progress             |Expiration       |
      #| Renewable - 6 Months    |Contract         |
      #| On Hold                 |Contract         |
      #| Bound - Pending Actions |Contract         |

  @regression
  Scenario Outline: Verify the Add Columns functionality within the grid by dragging columns from Tool Panel into grid
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the Tool menu in the column heading
    And I will be able to see tool menu popup
    And I click to select the Tool Panel option
    And I will be able to see Tool Panel displayed on the right side of the grid
    And I click to hold and drag the <columnname> columns into the grid
    Then I will be able to see the grid displaying the <columnname> column
    Examples:
      | status                  |columnname       |
      | In Progress             |Expiration       |
      #| Renewable - 6 Months    |Contract         |
      #| On Hold                 |Contract         |
      #| Bound - Pending Actions |Contract         |

  @Sprint3 @Done @regression
  Scenario Outline: Verify the panels without substatuses are disabled for selection when they have count of zero
    Given I open the browser and navigate to GRS link
    Then I will be able to see the <status> panel is grayed out and disabled if the count is zero and if available <substatus> substatus count is zero
    Examples:
      | status                  | substatus                                                                        |
      | In Progress             | Under Review,Authorize,Outstanding Quote,To Be Declined,Bound Pending Data Entry |
      | Renewable - 6 Months    |                                                                                  |
      | On Hold                 |                                                                                  |
      | Bound - Pending Actions |                                                                                  |
      #| DUMMY - QA USE Only     | Inquired,In Due Diligence,Informational Agreement                                |
      #| DUMMY - DEV USE Only    |                                                                                  |



  @Sprint3 @Done @regression
  Scenario Outline: Verify the panels with substatuses having count greater than zero are not disabled for selection even if all the substatuses are unselected
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I unselect all of the <status> status <substatus> substatuses having a count greater than zero
    Then I will be able to see the <status> status panel is not disabled for selecting
    Examples:
      | status                  | substatus                                                                        |
      | In Progress             | Under Review,Authorize,Outstanding Quote,To Be Declined,Bound Pending Data Entry |
#      | DUMMY - QA USE Only     | Inquired,In Due Diligence,Informational Agreement                                |
#      | DUMMY - DEV USE Only    |                                                                                  |

  @Sprint3 @Done @regression
  Scenario Outline: Verify deal grid is populated as per the selected multiple panels on selecting
    Given I open the browser and navigate to GRS link and select on the <status1> and <status2> Panel
    Then the deals details displayed has the deals displayed with the status as <status1> or <status2>
    Examples:
      | status1     | status2 |
      | In Progress | On Hold |

  @Sprint5 @Done @regression
  Scenario Outline: Verify deal grid is populated as per the selected multiple panels on selecting with ctrl select and also unselection with ctrl
    Given I open the browser and navigate to GRS link and ctrl select on the <status1> and <status2> Panel
    Then the deals details displayed has the deals displayed with the status as <status1> or <status2>
    Then I ctrl unselect the <status1> and the <status2> only remains selected and the deals displayed is of the selected status
    Examples:
      | status1     | status2 |
      | In Progress | On Hold |

  @Sprint5 @Done @regression
  Scenario Outline: Verify previously selected panels are unselected on selecting Renewawable - 6 Months panel with ctrl select
    Given I open the browser and navigate to GRS link and ctrl select on the <status1> and <status2> Panel
    And I ctrl click on the <status3> status panel
    Then I will be able to see previously selected <status1> and <status2> status panels get unselected and <status3> panel gets selected
    Examples:
      | status1     | status2 | status3              |
      | In Progress | On Hold | Renewable - 6 Months |

  @Sprint3 @Done @regression
  Scenario Outline: Verify previously selected panels are unselected on selecting Renewawable - 6 Months panel
    Given I open the browser and navigate to GRS link and select on the <status1> and <status2> Panel
    And I click on the <status3> status panel
    Then I will be able to see previously selected <status1> and <status2> status panels get unselected and <status3> panel gets selected
    Examples:
      | status1     | status2 | status3              |
      | In Progress | On Hold | Renewable - 6 Months |

  @Sprint5 @Done @regression
  Scenario Outline: Verify the selected Renewable - 6 Months panel gets unselected on selecting other panels with ctrl select
    Given I open the browser and navigate to GRS link
    And I ctrl click on the <status2> status panel
    And I ctrl click on the <status1> status panel
    Then I will be able to see <status2> panel gets unselected and <status1> status panel get selected
    Examples:
      | status1     | status2              |
      | In Progress | Renewable - 6 Months |

  @Sprint3 @Done @regression
  Scenario Outline: Verify the selected Renewable - 6 Months panel gets unselected on selecting other panels
    Given I open the browser and navigate to GRS link
    And I click on the <status2> status panel
    And I click on the <status1> status panel
    Then I will be able to see <status2> panel gets unselected and <status1> status panel get selected
    Examples:
      | status1     | status2              |
      | In Progress | Renewable - 6 Months |

  @Sprint5 @Done @regression
  Scenario Outline: Verify the selected panel gets unselected on selecting other panels with out ctrl for the second selection
    Given I open the browser and navigate to GRS link
    And I click on the <status2> status panel
    And I click on the <status1> status panel
    Then I will be able to see <status2> panel gets unselected and <status1> status panel get selected
    Examples:
      | status1     | status2 |
      | In Progress | On Hold |

  @Sprint3 @Done @regression
  Scenario Outline: Verify the grid refreshes each time the count on the selected panel changes
    Given I open the browser and navigate to GRS link and ctrl select on the <status1> and <status2> Panel
    Then I will be able to see the counts of status panels refreshes with most recent counts
    Examples:
      | status1     | status2 |
      | In Progress | On Hold |

  @Sprint4 @Done @regression
  Scenario Outline: Verify the Filter Columns functionality for different columns and applicable operator combinations except status
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    Then I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string and <operator> operator selected
    Examples:
      | status      | columnname  | operator     | textvalue                         |
      | In Progress | Deal Number | Equals       | 1369797                           |
      | In Progress | Deal Name   | Equals       | Yates Construction Company - 2018 |
      | In Progress | Inception   | Equals       | 2018-03-01                        |
      | In Progress | Deal Number | Not equal    | 1369797                           |
      | In Progress | Deal Name   | Not equal    | Yates Construction Company - 2018 |
      | In Progress | Inception   | Not equal    | 2018-03-01                        |
      | In Progress | Deal Name   | Contains     | Company                           |
      | In Progress | Deal Name   | Not contains | Company                           |

  @Sprint4 @Done @regression
  Scenario Outline: Verify the Filter Columns functionality for status column and available selection based on status
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the corresponding <selection> selection
    Then I will be able to see grid is filtered as per the <columnname> column and <selection> selection
    Examples:
      | status      | columnname | selection                        |
      | In Progress | Status     | Authorize                        |
      | On Hold     | Status     | On Hold                          |
      #| In Progress | Deal Name  | Under Review                     |
      #| In Progress | Inception  | Outstanding Quote,To Be Declined |

  @Sprint4 @regression
  Scenario Outline: Verify the automatic selection of default team options for mentioned user type
    Given I open the browser and navigate as <user> user to GRS link
    And I click on the Team field
    Then I will be able to see default team options used by <user> user are automatically selected
    Examples:
      | user       |
      | PTA        |
      | UW Manager |

  @Sprint5 @firefox @regression
  Scenario Outline: Verify that if I apply Teams filter and export the data in Excel, the data is exported based on the filter
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I right click on the grid click on the Export option and select the Excel Export option
    Then UI grid data should be exported in Excel format
    Examples:
      | status                  |
      | In Progress             |
      #| Renewable - 6 Months    |
      #| On Hold                 |
      #| Bound - Pending Actions |

  @Sprint5 @regression @Done
  Scenario Outline: Verify whether the needed fields are editable in the GRS for user and the deal details are successfully updated in DB
    Given I open the browser and navigate as <user> user to GRS link and click on the <panelstatus> Panel
    And I filter the deal as a <user> user and click on a deal having the dealnumber <dealnumber> and I see the deal details displayed on the right side of the screen
    And as a <user> user I see the mentioned fields non editable on the page
    And as a <user> user I edit the fields with the mentioned values <dealname> as Deal Name, <targetdate> as Target Date, <priority> as Priority, <status> as Status, <primaryUnderwriter> as primary Underwriter, <secondaryUnderwriter> as secondary Underwriter, <ta> as Technical Assistant, <modeler> as Modelername, <actuary> as Actuaryname
    And as a <user> user I am able to see the deal having the dealnumber <dealnumber> updated successfully in DB with the new details <dealname> as Deal Name, <targetdate> as Target Date, <priority> as Priority, <status> as Status, <primaryUnderwriter> as primary Underwriter, <secondaryUnderwriter> as secondary Underwriter, <ta> as Technical Assistant, <modeler> as Modelername, <actuary> as Actuaryname
    And as a <user> user I click the cancel button to go back to the deal details grid having the deal with deal number <dealnumber> under the <panelstatus> panel
    And as a <user> user I click on a deal having the dealnumber <dealnumber> and I see the deal details displayed on the right side of the screen
    Then as a <user> user I reset the deal values with the actual values <actualvalues>
    Examples:
      | user             | panelstatus | dealnumber | dealname           | targetdate | priority | status       | primaryUnderwriter | secondaryUnderwriter | ta             | modeler     | actuary     | actualvalues                                                                                         |
      | All Access       | In Progress | 1369776    | Modified Deal Name | 12/31/2018 | 234      | Under Review | Fred Ruck          | Amy Gimbel           | Elena Marshall | Makai Joell | Steve Meyer | CCG Services, Inc. aka Core Construction - 2018*12/31/1969*0*Authorize*Diana Lafemina**Kate Trent*** |
      | Read Only Access | In Progress | 1369776    | Modified Deal Name | 12/31/2018 | 234      | Under Review | Fred Ruck          | Amy Gimbel           | Elena Marshall | Makai Joell | Steve Meyer | CCG Services, Inc. aka Core Construction - 2018*12/31/1969*0*Authorize*Diana Lafemina**Kate Trent*** |

  @Sprint5 @regression @Done
  Scenario Outline: Verify whether the needed fields are editable and target date using date picker in the GRS for user and the deal details are successfully updated in DB
    Given I open the browser and navigate as <user> user to GRS link and click on the <panelstatus> Panel
    And I filter the deal as a <user> user and click on a deal having the dealnumber <dealnumber> and I see the deal details displayed on the right side of the screen
    And as a <user> user I see the mentioned fields non editable on the page
    And as a <user> user I edit the fields with the mentioned values <dealname> as Deal Name, <targetdate> as Target Date using date picker, <priority> as Priority, <status> as Status, <primaryUnderwriter> as primary Underwriter, <secondaryUnderwriter> as secondary Underwriter, <ta> as Technical Assistant, <modeler> as Modelername, <actuary> as Actuaryname
    And as a <user> user I am able to see the deal having the dealnumber <dealnumber> updated successfully in DB with the new details <dealname> as Deal Name, <targetdate> as Target Date, <priority> as Priority, <status> as Status, <primaryUnderwriter> as primary Underwriter, <secondaryUnderwriter> as secondary Underwriter, <ta> as Technical Assistant, <modeler> as Modelername, <actuary> as Actuaryname
    And as a <user> user I click the cancel button to go back to the deal details grid having the deal with deal number <dealnumber> under the <panelstatus> panel
    And as a <user> user I click on a deal having the dealnumber <dealnumber> and I see the deal details displayed on the right side of the screen
    Then as a <user> user I reset the deal values with the actual values <actualvalues>
    Examples:
      | user             | panelstatus | dealnumber | dealname           | targetdate | priority | status       | primaryUnderwriter | secondaryUnderwriter | ta             | modeler     | actuary     | actualvalues                                                                                         |
      | All Access       | In Progress | 1369776    | Modified Deal Name | 12/31/2018 | 234      | Under Review | Fred Ruck          | Amy Gimbel           | Elena Marshall | Makai Joell | Steve Meyer | CCG Services, Inc. aka Core Construction - 2018*12/31/1969*0*Authorize*Diana Lafemina**Kate Trent*** |

  @Sprint5 @regression
  Scenario Outline: Verify the Time Element filter functionality on GRS Home page
    Given I open the browser and navigate as <user> user to GRS link and click on the <status> Panel
    And I will be able to see the Time Element filter above the grid
    And I click on the <timeElement> filter
    Then I will be able to see the grid data filter as per the <timeElement> filter
    Examples:
      | status      | user       | timeElement    |
      | In Progress | All Access | Past Inception |
#      | In Progress | All Access | Over 30 Days   |
#      | In Progress | All Access | Within 30 Days |

  @regression
  Scenario Outline: Verify the Time counter and  its functionality on the GRS Homepage
    Given I open the browser and navigate to GRS link
    And I will be able to see a Time Counter on the page
    And I can see that the Time Counter is incrementing in minutes
    Then the time lapsed in the Time Counter should match with the time lapsed in the System clock
    And I click on the <status> status panel
    Then I will be able to see the counts of status panels refreshes with most recent counts
    Then the counter should start from zero and continue incrementing
    Examples:
      | status  |
      | On Hold |


