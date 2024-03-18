import { Action, createReducer, on } from '@ngrx/store';
import { WorkItem } from '../../models';
import { ApiRequestStatus, WorkItemState } from '../state/work-item.state';
import * as workItemActions from '../actions/work-item.actions';

export const initialState: WorkItemState = {
  workItems: [],
  selectedWorkItem: new WorkItem(),
  getWorkItemStatus: undefined,
  createWorkItemStatus: undefined,
  updateWorkItemStatus: undefined,
  createUpdateErrorMessage: undefined,
};

const reducer = createReducer(
  initialState,

  // select work item
  on(workItemActions.selectWorkItem, (state, { payload }) => ({
    ...state,
    selectedWorkItem: payload,
  })),

  // Get work items
  on(workItemActions.getWorkItems, (state) => ({
    ...state,
    getWorkItemStatus: ApiRequestStatus.Requesting,
  })),
  on(workItemActions.getWorkItemsSuccess, (state, { payload }) => ({
    ...state,
    getWorkItemStatus: ApiRequestStatus.Success,
    workItems: payload,
  })),
  on(workItemActions.getWorkItemsFail, (state) => ({
    ...state,
    getWorkItemStatus: ApiRequestStatus.Failed,
  })),

  // Create work item
  on(workItemActions.createWorkItem, (state) => ({
    ...state,
    createWorkItemStatus: ApiRequestStatus.Requesting,
  })),
  on(workItemActions.createWorkItemSuccess, (state, { payload }) => ({
    ...state,
    createWorkItemStatus: ApiRequestStatus.Success,
    workItems: [payload, ...state.workItems],
  })),
  on(workItemActions.createWorkItemFail, (state) => ({
    ...state,
    createWorkItemStatus: ApiRequestStatus.Failed,
    createUpdateErrorMessage: 'Failed to create new item. Please try again.'
  })),

  // Update work item
  on(workItemActions.updateWorkItem, (state) => ({
    ...state,
    updateWorkItemStatus: ApiRequestStatus.Requesting,
    createUpdateErrorMessage: undefined
  })),
  on(workItemActions.updateWorkItemSuccess, (state, { payload }) => ({
    ...state,
    updateWorkItemStatus: ApiRequestStatus.Success,
    workItems: state.workItems.map((i) => (i.id === payload.id ? payload : i)),
  })),
  on(workItemActions.updateWorkItemFail, (state) => ({
    ...state,
    updateWorkItemStatus: ApiRequestStatus.Failed,
    createUpdateErrorMessage: 'Failed to update new item. Please try again.'
  })),

  // Delete work item
  on(workItemActions.deleteWorkItem, (state) => ({
    ...state,
    deleteWorkItemStatus: ApiRequestStatus.Requesting,
  })),
  on(workItemActions.deleteWorkItemSuccess, (state, { payload }) => ({
    ...state,
    deleteWorkItemStatus: ApiRequestStatus.Success,
    workItems: state.workItems.filter((i) => i.id !== payload.id),
  })),
  on(workItemActions.deleteWorkItemFail, (state) => ({
    ...state,
    deleteWorkItemStatus: ApiRequestStatus.Failed,
  }))
);

export function workItemReducer(state: WorkItemState | undefined, action: Action) {
  return reducer(state, action);
}
