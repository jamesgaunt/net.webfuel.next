import { Injectable } from '@angular/core';
import { IDataSource } from 'shared/common/data-source';
import { StaticDataService } from '../core/static-data.service';
import { FundingBody, FundingStream, Gender, ProjectStatus, ResearchMethodology, SubmissionStage, SuportRequestStatus, Title } from './api.types';

@Injectable()
export class StaticDataCache {
    
    constructor(private staticDataService: StaticDataService) {
    }
    
    fundingBody: IDataSource<FundingBody> = { query: (query) => this.staticDataService.load(query, s => s.fundingBody) };
    
    fundingStream: IDataSource<FundingStream> = { query: (query) => this.staticDataService.load(query, s => s.fundingStream) };
    
    gender: IDataSource<Gender> = { query: (query) => this.staticDataService.load(query, s => s.gender) };
    
    projectStatus: IDataSource<ProjectStatus> = { query: (query) => this.staticDataService.load(query, s => s.projectStatus) };
    
    researchMethodology: IDataSource<ResearchMethodology> = { query: (query) => this.staticDataService.load(query, s => s.researchMethodology) };
    
    submissionStage: IDataSource<SubmissionStage> = { query: (query) => this.staticDataService.load(query, s => s.submissionStage) };
    
    suportRequestStatus: IDataSource<SuportRequestStatus> = { query: (query) => this.staticDataService.load(query, s => s.suportRequestStatus) };
    
    title: IDataSource<Title> = { query: (query) => this.staticDataService.load(query, s => s.title) };
}

