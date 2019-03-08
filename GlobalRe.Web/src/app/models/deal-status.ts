export class DealStatus {
  statusCode?: number;
  statusName: string;
  count: number;
  sortOrder: number;
  workflowId: number;
  workflowName: string;
  isDisabled: boolean;
  isSelected: boolean;
  statusSummary?: DealStatus[];
  isCtrlKey?: boolean;
  dealNumber?: number;

  constructor(target?:{statusCode?: number;
    statusName: string;
    count: number;
    sortOrder: number;
    workflowId: number;
    workflowName: string;
    isDisabled: boolean;
    isSelected: boolean;
    statusSummary?: DealStatus[];
    isCtrlKey?: boolean;}) {
    this.statusCode = target.statusCode;
    this.statusName = target.statusName;
    this.count = target.count;
    this.sortOrder = target.sortOrder;
    this.workflowId = target.workflowId;
    this.workflowName = target.workflowName;
    this.isDisabled = (target.isDisabled) ? target.isDisabled : false;
    this.statusSummary = target.statusSummary;
    this.isCtrlKey = target.isCtrlKey || false;
  }
}
