import { WorkItem } from '../../models';

export enum ApiRequestStatus {
  Requesting = 'Requesting',
  Success = 'Success',
  Failed = 'Failed',
}

export interface WorkItemState {
  selectedWorkItem: WorkItem;
  workItems: WorkItem[];
  getWorkItemStatus: ApiRequestStatus | undefined;
  createWorkItemStatus: ApiRequestStatus | undefined;
  updateWorkItemStatus: ApiRequestStatus | undefined;
  createUpdateErrorMessage: string | undefined;
}
