import { Injectable } from '@angular/core';
import { IDataSource, IDataSourceWithGet } from 'shared/common/data-source';
import { StaticDataService } from '../core/static-data.service';
import { AgeRange, ApplicationStage, Disability, Ethnicity, FundingBody, FundingCallType, FundingStream, Gender, HowDidYouFindUs, IsCTUAlreadyInvolved, IsCTUTeamContribution, IsFellowship, IsInternationalMultiSiteStudy, IsLeadApplicantNHS, IsPaidRSSAdviserCoapplicant, IsPaidRSSAdviserLead, IsPPIEAndEDIContribution, IsPrePostAward, IsQuantativeTeamContribution, IsResubmission, IsTeamMembersConsulted, IsYesNo, ProfessionalBackground, ProfessionalBackgroundDetail, ProjectStatus, ReportProvider, ResearcherCareerStage, ResearcherLocation, ResearcherOrganisationType, ResearcherRole, ResearchMethodology, RSSHub, Site, StaffRole, SubmissionOutcome, SubmissionStage, SubmissionStatus, SupportProvided, SupportRequestStatus, SupportTeam, Title, UserDiscipline, WillStudyUseCTU, WorkActivity } from './api.types';

@Injectable()
export class StaticDataCache {
    
    constructor(private staticDataService: StaticDataService) {
    }
    
    ageRange: IDataSourceWithGet<AgeRange> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.ageRange),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.ageRange),
    };
    
    applicationStage: IDataSourceWithGet<ApplicationStage> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.applicationStage),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.applicationStage),
    };
    
    disability: IDataSourceWithGet<Disability> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.disability),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.disability),
    };
    
    ethnicity: IDataSourceWithGet<Ethnicity> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.ethnicity),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.ethnicity),
    };
    
    fundingBody: IDataSourceWithGet<FundingBody> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.fundingBody),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.fundingBody),
    };
    
    fundingCallType: IDataSourceWithGet<FundingCallType> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.fundingCallType),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.fundingCallType),
    };
    
    fundingStream: IDataSourceWithGet<FundingStream> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.fundingStream),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.fundingStream),
    };
    
    gender: IDataSourceWithGet<Gender> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.gender),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.gender),
    };
    
    howDidYouFindUs: IDataSourceWithGet<HowDidYouFindUs> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.howDidYouFindUs),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.howDidYouFindUs),
    };
    
    isCTUAlreadyInvolved: IDataSourceWithGet<IsCTUAlreadyInvolved> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isCTUAlreadyInvolved),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isCTUAlreadyInvolved),
    };
    
    isCTUTeamContribution: IDataSourceWithGet<IsCTUTeamContribution> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isCTUTeamContribution),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isCTUTeamContribution),
    };
    
    isFellowship: IDataSourceWithGet<IsFellowship> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isFellowship),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isFellowship),
    };
    
    isInternationalMultiSiteStudy: IDataSourceWithGet<IsInternationalMultiSiteStudy> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isInternationalMultiSiteStudy),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isInternationalMultiSiteStudy),
    };
    
    isLeadApplicantNHS: IDataSourceWithGet<IsLeadApplicantNHS> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isLeadApplicantNHS),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isLeadApplicantNHS),
    };
    
    isPaidRSSAdviserCoapplicant: IDataSourceWithGet<IsPaidRSSAdviserCoapplicant> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isPaidRSSAdviserCoapplicant),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isPaidRSSAdviserCoapplicant),
    };
    
    isPaidRSSAdviserLead: IDataSourceWithGet<IsPaidRSSAdviserLead> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isPaidRSSAdviserLead),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isPaidRSSAdviserLead),
    };
    
    isPPIEAndEDIContribution: IDataSourceWithGet<IsPPIEAndEDIContribution> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isPPIEAndEDIContribution),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isPPIEAndEDIContribution),
    };
    
    isPrePostAward: IDataSourceWithGet<IsPrePostAward> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isPrePostAward),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isPrePostAward),
    };
    
    isQuantativeTeamContribution: IDataSourceWithGet<IsQuantativeTeamContribution> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isQuantativeTeamContribution),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isQuantativeTeamContribution),
    };
    
    isResubmission: IDataSourceWithGet<IsResubmission> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isResubmission),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isResubmission),
    };
    
    isTeamMembersConsulted: IDataSourceWithGet<IsTeamMembersConsulted> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isTeamMembersConsulted),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isTeamMembersConsulted),
    };
    
    isYesNo: IDataSourceWithGet<IsYesNo> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.isYesNo),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.isYesNo),
    };
    
    professionalBackground: IDataSourceWithGet<ProfessionalBackground> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.professionalBackground),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.professionalBackground),
    };
    
    professionalBackgroundDetail: IDataSourceWithGet<ProfessionalBackgroundDetail> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.professionalBackgroundDetail),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.professionalBackgroundDetail),
    };
    
    projectStatus: IDataSourceWithGet<ProjectStatus> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.projectStatus),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.projectStatus),
    };
    
    reportProvider: IDataSourceWithGet<ReportProvider> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.reportProvider),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.reportProvider),
    };
    
    researcherCareerStage: IDataSourceWithGet<ResearcherCareerStage> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.researcherCareerStage),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.researcherCareerStage),
    };
    
    researcherLocation: IDataSourceWithGet<ResearcherLocation> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.researcherLocation),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.researcherLocation),
    };
    
    researcherOrganisationType: IDataSourceWithGet<ResearcherOrganisationType> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.researcherOrganisationType),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.researcherOrganisationType),
    };
    
    researcherRole: IDataSourceWithGet<ResearcherRole> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.researcherRole),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.researcherRole),
    };
    
    researchMethodology: IDataSourceWithGet<ResearchMethodology> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.researchMethodology),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.researchMethodology),
    };
    
    rssHub: IDataSourceWithGet<RSSHub> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.rssHub),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.rssHub),
    };
    
    site: IDataSourceWithGet<Site> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.site),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.site),
    };
    
    staffRole: IDataSourceWithGet<StaffRole> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.staffRole),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.staffRole),
    };
    
    submissionOutcome: IDataSourceWithGet<SubmissionOutcome> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.submissionOutcome),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.submissionOutcome),
    };
    
    submissionStage: IDataSourceWithGet<SubmissionStage> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.submissionStage),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.submissionStage),
    };
    
    submissionStatus: IDataSourceWithGet<SubmissionStatus> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.submissionStatus),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.submissionStatus),
    };
    
    supportProvided: IDataSourceWithGet<SupportProvided> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.supportProvided),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.supportProvided),
    };
    
    supportRequestStatus: IDataSourceWithGet<SupportRequestStatus> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.supportRequestStatus),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.supportRequestStatus),
    };
    
    supportTeam: IDataSourceWithGet<SupportTeam> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.supportTeam),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.supportTeam),
    };
    
    title: IDataSourceWithGet<Title> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.title),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.title),
    };
    
    userDiscipline: IDataSourceWithGet<UserDiscipline> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.userDiscipline),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.userDiscipline),
    };
    
    willStudyUseCTU: IDataSourceWithGet<WillStudyUseCTU> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.willStudyUseCTU),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.willStudyUseCTU),
    };
    
    workActivity: IDataSourceWithGet<WorkActivity> = {
        query: (query) => this.staticDataService.queryFactory(query, s => s.workActivity),
        get: (params: { id: string }) => this.staticDataService.getFactory(params.id, s => s.workActivity),
    };
}

