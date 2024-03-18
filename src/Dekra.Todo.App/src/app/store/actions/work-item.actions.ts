import { createAction, props } from '@ngrx/store';
import { WorkItem } from '../../models';


export const setSelectWorkItem = createAction('[WorkItem] Set selected item', props<{ payload: WorkItem }>());


export const getWorkItems = createAction('[WorkItem] Get List');

export const getWorkItemsSuccess = createAction('[WorkItem] Get List Success', props<{ payload: WorkItem[] }>());

export const getWorkItemsFail = createAction('[WorkItem] Get List Fail');


export const createWorkItem = createAction('[WorkItem] Create work item', props<{ payload: WorkItem }>());

export const createWorkItemSuccess = createAction('[WorkItem] Create new work item Success', props<{ payload: WorkItem }>());

export const createWorkItemFail = createAction('[WorkItem] Create new work item Fail');


export const updateWorkItem = createAction('[WorkItem] Update work item', props<{ payload: WorkItem }>());

export const updateWorkItemSuccess = createAction('[WorkItem] Update new work item Success', props<{ payload: WorkItem }>());

export const updateWorkItemFail = createAction('[WorkItem] Update new work item Fail');


export const deleteWorkItem = createAction('[WorkItem] Delete work item', props<{ payload: WorkItem }>());

export const deleteWorkItemSuccess = createAction('[WorkItem] Delete new work item Success', props<{ payload: WorkItem }>());

export const deleteWorkItemFail = createAction('[WorkItem] Delete new work item Fail');
