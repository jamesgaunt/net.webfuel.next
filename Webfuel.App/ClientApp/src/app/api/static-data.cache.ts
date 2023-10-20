import { Injectable } from '@angular/core';
import { IDataSource } from 'shared/common/data-source';
import { StaticDataService } from '../core/static-data.service';
import { ApplicationStage, FundingBody, FundingCallType, FundingStream, Gender, ProjectStatus, ResearchMethodology, SubmissionStage, SupportRequestStatus, Title } from './api.types';

@Injectable()
export class StaticDataCache {
    
    constructor(private staticDataService: StaticDataService) {
    }
    
    applicationStage: IDataSource<ApplicationStage> = { query: (query) => this.staticDataService.load(query, s => s.applicationStage) };
    
    fundingBody: IDataSource<FundingBody> = { query: (query) => this.staticDataService.load(query, s => s.fundingBody) };
    
    fundingCallType: IDataSource<FundingCallType> = { query: (query) => this.staticDataService.load(query, s => s.fundingCallType) };
    
    fundingStream: IDataSource<FundingStream> = { query: (query) => this.staticDataService.load(query, s => s.fundingStream) };
    
    gender: IDataSource<Gender> = { query: (query) => this.staticDataService.load(query, s => s.gender) };
    
    projectStatus: IDataSource<ProjectStatus> = { query: (query) => this.staticDataService.load(query, s => s.projectStatus) };
    
    researchMethodology: IDataSource<ResearchMethodology> = { query: (query) => this.staticDataService.load(query, s => s.researchMethodology) };
    
    submissionStage: IDataSource<SubmissionStage> = { query: (query) => this.staticDataService.load(query, s => s.submissionStage) };
    
    supportRequestStatus: IDataSource<SupportRequestStatus> = { query: (query) => this.staticDataService.load(query, s => s.supportRequestStatus) };
    
    title: IDataSource<Title> = { query: (query) => this.staticDataService.load(query, s => s.title) };
}

