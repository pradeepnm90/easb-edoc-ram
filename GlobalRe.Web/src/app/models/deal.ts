import { Note } from "./note";

export interface Deal {
  dealNumber: number;
  dealName: string;
  statusCode: any;
  status: string;
  contractNumber: number;
  inceptionDate: any;
  targetDate?: any;
  priority?: any;
  submittedDate?: any;
  primaryUnderwriterCode: any;
  primaryUnderwriterName: string;
  secondaryUnderwriterCode?: any;
  secondaryUnderwriterName?: any;
  technicalAssistantCode?: any;
  technicalAssistantName?: string;
  modellerCode?: any;
  modellerName?: any;
  actuaryCode?: any;
  actuaryName?: any;
  expiryDate: any;
  brokerCode?: number;
  brokerName?: string;
  brokerContactCode?: number;
  brokerContactName?: string;
  action: string;
}

