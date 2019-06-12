% Collects all Behavioral Data
clear; close all

S=1; %Trail to Start
F=200; %Trial to Stop
Sub=30; %Participant Number
Session=1; %Session Day
Parnum=['Sub' num2str(Sub)];
cs=350; %Number of time steps
t=(1:350)./100; %Time for plotting
%For saving data across participants
%Switch to True if you want to save
%Be sure to set cd accordingly, line 197
storeRev=false;
storeAC=false;
storeOut=false;
storeTar=false;
storeSC=false;

%Import data from Unity Folder
%This script follows the file naming convention of
%RecPosP00XS00Y\position.txtZ
%X = Participantion number Sub
%Y = Session Day
%Z = trail number
for i = S:F
    FileName= ['G:\My Drive\Study 1\Data\Game\Independent Set\RecPosP00',(num2str(Sub,'%d')),'S00',(num2str(Session,'%d')),'\position.txt',(num2str(i,'%d'))];
    
    FileName1= ['G:\My Drive\Study 1\Data\Game\Independent Set\RecAccP00',(num2str(Sub,'%d')),'S00',(num2str(Session,'%d')),'\position.txt',(num2str(i,'%d'))];
    
    y=dlmread(FileName1,';');
    %If statements ensure that data is same length across all trials
    y=y(:,1);
    if length(y)<=499
        z=500-length(y);
        y=[y; zeros(z,1)];
    end
    y=y(1:500,1);
    RawAcc(:,i)=y;
    
    
    
    x=dlmread(FileName,';');
    x=x(:,1);
    if length(x)<=499
        z=500-length(x);
        x=[x; zeros(z,1)];
    end
    x=x(1:500,1);
    Raw(:,i)=x;
    
end


%Determine Scaling Ratio, Negative/Positive Acceleration, for each trial
Sca=(sum(RawAcc==-7.5)./sum(RawAcc==7.5));
Sca(isnan(Sca))=0; %Scaling Ratio of NA is converted to 0
Sca(Sca>2)=0; %Scaling Ratio over 2 converted to 0. Typically means they went backwards.
mSca=mean(Sca);

%Find when positive acceleration is stopped and negative acceleration is
%applied.
pos=zeros(size(RawAcc,2),300)';
neg=zeros(size(RawAcc,2),300)';
for i = 1:size(RawAcc,2)
    %find all positve ACC time points
    pos(i,1:length(find(RawAcc(50:cs,i)>0)'))=find(RawAcc(50:cs,i)>0)';
    %find all negative ACC time points
    neg(i,1:length(find(RawAcc(50:cs,i)<0)'))=find(RawAcc(50:cs,i)<0)';
    %Find time point that reverse is first made
    RevN(i)=neg(i,1);
    %Find last postive ACC before first negative ACC
    ptmp=find(pos(i,:)<neg(i,1) & pos(i,:)>0);
    if isempty(ptmp)
        ptmp=max(pos(i,:));
    end
    PosStop(i)=max(ptmp);
    clear ptmp
end


if Session == 1 && Sub == 1
    mat=['PosStop',(num2str(Sub,'%d'))];
    mat1=['RevN',(num2str(Sub,'%d'))];
    
    if storeRev
        str=[mat '=PosStop;'];
        str1=[mat1 '=RevN;'];
        eval(str);
        eval(str1);
        save('GroupReverse.mat',['PosStop',(num2str(Sub,'%d'))],['RevN',(num2str(Sub,'%d'))])
    end
elseif Session == 1 && Sub > 1
    mat=['PosStop',(num2str(Sub,'%d'))];
    mat1=['RevN',(num2str(Sub,'%d'))];
    
    if storeRev
        str=[mat '=PosStop;'];
        str1=[mat1 '=RevN;'];
        eval(str);
        eval(str1);
        save('GroupReverse.mat',['PosStop',(num2str(Sub,'%d'))],['RevN',(num2str(Sub,'%d'))],'-append')
    end
end


%Determine time in target and time spent outside the startbox (proxy of
%reaction time)
for i = S:F
    %RawAcc_p(:,i)=RawAcc;
    
    %Finding time in target
    Desired=Raw(1:cs,i);
    N = 100; % Required number of consecutive numbers following a first one
    %Desired1=Desired(Desired~=0);
    p=find(Desired>=14.75 & Desired<=17.25); %x axis of the stopbox
    x = diff(p')==1;
    
    
    %Add 0 at end so time can be counted later
    if isempty(x)==0
        x(end)=0;
    end
    
    %Total time in target
    Target(i)=sum(x);
    f=find(x==0);
    
    %Find the amount of time participant spend outside of box
    out(i)=length(find(Raw(1:cs,i)>1.25));
    
    clear ConTar %reset time in target for each trial in loop
    
    %Find all consecutive times in target
    if isempty(f) == 1
        ConTar=0;
    end
    
    if length(f)==1
        ConTar=f;
    else
        for j = length(f):-1:2
            ConTar(j)=f(j)-f(j-1);
        end
        
    end
    
    
    ConTarT(i)=max(ConTar);
    
    
    %Find if trial was successful
    for k=1:length(ConTar)
        if ConTar(k)>=100
            Goal(k)=1;
        else
            Goal(k)=0;
        end
    end
    
    if sum(Goal)>=1
        Suc=1;
    else
        Suc=0;
    end
    
    %Store which trial was successful
    Success(i)=Suc;
    
    %Begin Taking Derivatives
    Traw=Raw(1:cs,i);
    %Tdata_p=Xdata_p(1:cs,i);
end



%finds which trials are successful (q) and unsuccessful (p)
q=find(Success>0);
p=find(Success<1);

if sum(Success) > 0
    Class = 1;
else
    Class = 0;
end

ACr=Sca;


%%
results = [{'Parnum'} {'Class'} {'AvgOut'} {'OutSD}'} {'AvgTime'} {'AvgScale'} {'ScaSD'} {'Success'};
    {Parnum} Class mean(out) std(out) mean(ConTarT) mean(ACr) std(ACr) sum(Success)]


%%
%For saving results across subjects
cd('C:\Users\Andrew\Google Drive\Study 1\Project1')
if Session == 2
    mat=['ACr',(num2str(Sub,'%d'))];
    
    if storeAC
        str=[mat '=ACr;'];
        eval(str);
        save('GroupScale2.mat',['ACr',(num2str(Sub,'%d'))],'-append')
    end
    
    mat=['Success',(num2str(Sub,'%d'))];
    
    if storeSC
        str=[mat '=Success;'];
        eval(str);
        save('Success.mat',['Success',(num2str(Sub,'%d'))],'-append')
    end
    
    matOut=['Out',(num2str(Sub,'%d'))];
    
    
    if storeOut
        strOut=[matOut '=out;'];
        eval(strOut);
        save('GroupOut2.mat',['Out',(num2str(Sub,'%d'))],'-append')
    end
    
    matTar=['Tar',(num2str(Sub,'%d'))];
    
    if storeTar
        strTar=[matTar '=ConTarT;'];
        eval(strTar);
        save('GroupTar2.mat',['Tar',(num2str(Sub,'%d'))],'-append')
    end
end

if Session == 1
    mat=['ACr',(num2str(Sub,'%d'))];
    
    if storeAC
        str=[mat '=ACr;'];
        eval(str);
        save('GroupScale.mat',['ACr',(num2str(Sub,'%d'))],'-append')
    end
    
    matOut=['Out',(num2str(Sub,'%d'))];
    
    
    if storeOut
        strOut=[matOut '=out;'];
        eval(strOut);
        save('GroupOut.mat',['Out',(num2str(Sub,'%d'))],'-append')
    end
    
    matTar=['Tar',(num2str(Sub,'%d'))];
    
    if storeTar
        strTar=[matTar '=ConTarT;'];
        eval(strTar);
        save('GroupTarNew.mat',['Tar',(num2str(Sub,'%d'))],'-append')
    end
end


figure;
r=1;
b=0;
for i = 1:F
    plot(Raw(1:cs,i),'Color',[r 0 b])
    hold on
    r=r-1/F;
    b=b+1/F;
    title('All Trials by Position - Color is earlier (red) or later (blue) in practice')
    xlabel('Time')
    ylabel('Distance')
end

S=logical(Success);
F=logical(~Success);
Short=Raw(cs,:)<14.75;
Long=Raw(cs,:)>17.25;
F1=Raw(cs,F)>14.75 & Raw(cs,F)<17.25;
figure;
shadedErrorBar(t,mean(RawAcc(1:cs,S)'),std(RawAcc(1:cs,S)'),'b',1)
hold on
a=plot(t,mean(RawAcc(1:cs,S)'),'b','LineWidth',3);
shadedErrorBar(t,mean(RawAcc(1:cs,F)'),std(RawAcc(1:cs,F)'),'g',1)
b=plot(t,mean(RawAcc(1:cs,F)'),'g','LineWidth',3)
shadedErrorBar(t,mean(RawAcc(1:cs,Long)'),std(RawAcc(1:cs,Long)'),'r',1)
c=plot(t,mean(RawAcc(1:cs,Long)'),'r','LineWidth',3)
shadedErrorBar(t,mean(RawAcc(1:cs,F1)'),std(RawAcc(1:cs,F1)'),'k',1)
d=plot(t,mean(RawAcc(1:cs,F1)'),'k','LineWidth',3)
legend([a b c d],...
    ['Success ',num2str(sum(Success))],...
    ['Short ' num2str(sum(Short))],...
    ['Long ' num2str(sum(Long))],...
    ['Low Time ' num2str(sum(F1))])
xlabel('Time (sec)')
ylabel('Cursor Acceleration')

