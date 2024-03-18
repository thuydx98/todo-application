import { createSelector } from '@ngrx/store';
import { WorkItemState } from '../state/work-item.state';

const selectWorkItemState = (state: any): WorkItemState => state.workItem;

export const selectedWorkItem = createSelector(selectWorkItemState, (state) => state.selectedWorkItem);

export const workItems = createSelector(selectWorkItemState, (state) => state.workItems);
export const getWorkItemStatus = createSelector(selectWorkItemState, (state) => state.getWorkItemStatus);


export const createWorkItemStatus = createSelector(selectWorkItemState, (state) => state.createWorkItemStatus);
export const updateWorkItemStatus = createSelector(selectWorkItemState, (state) => state.updateWorkItemStatus);
export const createUpdateErrorMessage = createSelector(selectWorkItemState, (state) => state.createUpdateErrorMessage);

