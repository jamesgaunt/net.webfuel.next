import { Injectable } from '@angular/core';
import { IDataSource } from 'shared/common/data-source';
import { StaticDataService } from '../core/static-data.service';
import { ApplicationStage, FundingBody, FundingCallType, FundingStream, Gender, HowDidYouFindUs, IsCTUTeamContribution, IsFellowship, IsInternationalMultiSiteStudy, IsLeadApplicantNHS, IsPPIEAndEDIContribution, IsQuantativeTeamContribution, IsResubmission, IsTeamMembersConsulted, ProjectStatus, ResearchMethodology, SubmissionStage, SupportProvided, SupportRequestStatus, Title } from './api.types';

@Injectable()
export class StaticDataCache {
    
    constructor(private staticDataService: StaticDataService) {
    }
    
    applicationStage: IDataSource<ApplicationStage> = { query: (query) => this.staticDataService.load(query, s => s.applicationStage) };
    
    fundingBody: IDataSource<FundingBody> = { query: (query) => this.staticDataService.load(query, s => s.fundingBody) };
    
    fundingCallType: IDataSource<FundingCallType> = { query: (query) => this.staticDataService.load(query, s => s.fundingCallType) };
    
    fundingStream: IDataSource<FundingStream> = { query: (query) => this.staticDataService.load(query, s => s.fundingStream) };
    
    gender: IDataSource<Gender> = { query: (query) => this.staticDataService.load(query, s => s.gender) };
    
    howDidYouFindUs: IDataSource<HowDidYouFindUs> = { query: (query) => this.staticDataService.load(query, s => s.howDidYouFindUs) };
    
    isCTUTeamContribution: IDataSource<IsCTUTeamContribution> = { query: (query) => this.staticDataService.load(query, s => s.isCTUTeamContribution) };
    
    isFellowship: IDataSource<IsFellowship> = { query: (query) => this.staticDataService.load(query, s => s.isFellowship) };
    
    isInternationalMultiSiteStudy: IDataSource<IsInternationalMultiSiteStudy> = { query: (query) => this.staticDataService.load(query, s => s.isInternationalMultiSiteStudy) };
    
    isLeadApplicantNHS: IDataSource<IsLeadApplicantNHS> = { query: (query) => this.staticDataService.load(query, s => s.isLeadApplicantNHS) };
    
    isPPIEAndEDIContribution: IDataSource<IsPPIEAndEDIContribution> = { query: (query) => this.staticDataService.load(query, s => s.isPPIEAndEDIContribution) };
    
    isQuantativeTeamContribution: IDataSource<IsQuantativeTeamContribution> = { query: (query) => this.staticDataService.load(query, s => s.isQuantativeTeamContribution) };
    
    isResubmission: IDataSource<IsResubmission> = { query: (query) => this.staticDataService.load(query, s => s.isResubmission) };
    
    isTeamMembersConsulted: IDataSource<IsTeamMembersConsulted> = { query: (query) => this.staticDataService.load(query, s => s.isTeamMembersConsulted) };
    
    projectStatus: IDataSource<ProjectStatus> = { query: (query) => this.staticDataService.load(query, s => s.projectStatus) };
    
    researchMethodology: IDataSource<ResearchMethodology> = { query: (query) => this.staticDataService.load(query, s => s.researchMethodology) };
    
    submissionStage: IDataSource<SubmissionStage> = { query: (query) => this.staticDataService.load(query, s => s.submissionStage) };
    
    supportProvided: IDataSource<SupportProvided> = { query: (query) => this.staticDataService.load(query, s => s.supportProvided) };
    
    supportRequestStatus: IDataSource<SupportRequestStatus> = { query: (query) => this.staticDataService.load(query, s => s.supportRequestStatus) };
    
    title: IDataSource<Title> = { query: (query) => this.staticDataService.load(query, s => s.title) };
}

