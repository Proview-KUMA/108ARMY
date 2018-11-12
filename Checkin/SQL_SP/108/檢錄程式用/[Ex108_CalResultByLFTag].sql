USE [Center]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--建立[Ex108_CalResultByLFTag]--
--重新建立一隻新的sp，供108年1月1日後使用晶片結算成績計算--
CREATE procedure [dbo].[Ex108_CalResultByLFTag]
(
@date datetime,
@lf_tag varchar(10)
)
as
declare @status varchar(5)
declare @memo varchar(5)
declare @pushup smallint 
declare @pushupscore smallint
declare @situp smallint 
declare @situpscore smallint
declare @run smallint 
declare @runscore smallint
declare @age smallint
declare @gender nvarchar(3)
declare @repl nvarchar(3)

if @date <= GETDATE() and exists (select * from Result where [date] = @date and [LF_Tag_ID] = @lf_tag)
begin
   /* feed parameters */
   select @status = [status], @memo = [memo],@situp = [sit_ups],@pushup = [push_ups],
   @run = [run],@age = [age],@gender = [gender]
   --,@op_id=[op_id]
   from Result where [date] = @date and [LF_Tag_ID] = @lf_tag
   
   /* if status is already upload, return record */
   if @status = '104'
   begin
   select [sid]
      ,[id]
      ,[name]
      ,[dbo].[F_GetROCDate]([birth]) 'birth'
      ,[age]
      ,[gender]
      ,dbo.F_GetUnitTitle([unit_code]) 'unit_code'
      ,dbo.F_GetRank([rank_code]) 'rank_code'
      ,[height]
      ,[weight]
      ,CONVERT(decimal(8, 1), [BMI]) as  [BMI]
      ,CONVERT(decimal(8, 1), [bodyfat]) as [bodyfat]
      ,[sit_ups]
      ,[sit_ups_score]
      ,[push_ups]
      ,[push_ups_score]
      ,[run]
      ,[run_score]
      ,[dbo].[F_GetROCDate]([date]) 'date'
      ,dbo.F_GetCenterName([center_code]) 'center_name'
      ,[result]
      ,[status]
      ,[op_id]
      ,[clothesNum]
      ,[LF_Tag_ID]
      ,[UHF_Tag_ID]
      ,[code]
      ,[memo]
      from Result where [date] = @date and [LF_Tag_ID] = @lf_tag
   end
   
   if SUBSTRING(@status,1,1) = '2' --已上傳成績
   begin
   select [sid]
      ,[id]
      ,[name]
      ,[dbo].[F_GetROCDate]([birth]) 'birth'
      ,[age]
      ,[gender]
      ,dbo.F_GetUnitTitle([unit_code]) 'unit_code'
      ,dbo.F_GetRank([rank_code]) 'rank_code'
      ,[height]
      ,[weight]
      ,CONVERT(decimal(8, 1), [BMI]) as  [BMI]
      ,CONVERT(decimal(8, 1), [bodyfat]) as [bodyfat]
      ,[sit_ups]
      ,[sit_ups_score]
      ,[push_ups]
      ,[push_ups_score]
      ,[run]
      ,[run_score]
      ,[dbo].[F_GetROCDate]([date]) 'date'
      ,dbo.F_GetCenterName([center_code]) 'center_name'
      ,[result]
      ,[status]
      ,[op_id]
      ,[clothesNum]
      ,[LF_Tag_ID]
      ,[UHF_Tag_ID]
      ,[code]
      ,[memo]
      from Result where [date] = @date and [LF_Tag_ID] = @lf_tag
   end
   /* start cal result */
   else  --未上傳成績
   begin
     if @status in ('001','102','103','105')
     begin   --CONVERT(varchar(10),
     /* feed encryption standard */
     OPEN SYMMETRIC KEY centerKey99
     DECRYPTION BY CERTIFICATE centerCert99
     select item,
     CONVERT(smallint,  DECRYPTBYKEY([standard])) 'standard',
     CONVERT(smallint,  DECRYPTBYKEY([score])) 'score'
     into #triple from StandardEncrypt_108 where gender = @gender and @age <= agemax and @age >= agemin
     --2016-12-1將原有替代項目表格修改，欄位比照基本三項
     --2016-12-1舊版--
     --select item_id,
     --CONVERT(smallint,  DECRYPTBYKEY([Standard])) 'standard'  --(gender = (case when @gender = 'F' then N'女' else N'男' end))
     --into #repl from ReplaceStandardEncrypt where gender = @gender and @age <= [end] and @age >= [start]
     --2016-12-1新版
     select item,
     CONVERT(smallint,  DECRYPTBYKEY([standard])) 'standard',
     CONVERT(smallint,  DECRYPTBYKEY([score])) 'score'
     into #repl from ReplaceStandardEncrypt_108 where gender = @gender and @age <= agemax and @age >= agemin
     CLOSE ALL SYMMETRIC KEYS
     
     --以下開始將次數及秒數換算成績--
     /* item sit ups */
     if @situp is null or @situp=9999  --斷判是不是空值或未完測
     begin 
      set @repl = SUBSTRING(@memo,1,1)
      --if @repl = '0' or @repl != 'G' or @repl != 'F'
      if @repl in('0','H','I','J')
      set @situpscore = 999
      else
      set @situpscore = 9999
     end
     else  --不是空值
     begin
     if SUBSTRING(@memo,1,1) = '0' --如果memo第一項是0
       begin
         if exists --判斷有沒有達到最低標準
         (select top 1 * from #triple where item = 'sit_ups' and @situp >= [standard] order by [standard] desc)
         begin
         set @situpscore = (select top 1 [score] from #triple where item = 'sit_ups' and @situp >= [standard] order by [standard] desc)
         end
         else  --如果沒達到最低標準則給予最低分
         begin
         set @situpscore = (select min([score]) from #triple where item = 'sit_ups')
         end
     end
     else  --如果第一項不是0
     --2016-11-30這裡判斷式要改，替代項目要加入成績
       begin
     set @repl = SUBSTRING(@memo,1,1)
     
     --新版--
     if @repl = 'F' or @repl = 'G'
     begin
      if exists --判斷有沒有達到最低標準
         (select top 1 * from #repl where [item] = @repl and @situp <= [standard] order by [standard])
         begin
         set @situpscore = (select top 1 [score] from #repl where [item] = @repl and @situp <= [standard] order by [standard])
         end
         else  --如果沒達到最低標準則給予最低分
         begin
         set @situpscore = (select min([score]) from #repl where [item] = @repl)
         end
     end
     else
     begin
      if exists --判斷有沒有達到最低標準
         (select top 1 * from #repl where [item] = @repl and @situp >= [standard] order by [standard] desc)
         begin
         set @situpscore = (select top 1 [score] from #repl where [item] = @repl and @situp >= [standard] order by [standard] desc)
         end
         else  --如果沒達到最低標準則給予最低分
         begin
         set @situpscore = (select min([score]) from #repl where [item] = @repl)
         end
     end
        
   end
   end
     
     
     /* item psuh ups */
     if @pushup is null or @pushup=9999
     begin
       set @repl = SUBSTRING(@memo,2,1)
       --if @repl = '0' or @repl != 'G' or @repl != 'F'
      if @repl in('0','H','I','J')
         set @pushupscore = 999
       else
         set @pushupscore = 9999
      end
     else
     begin
       if SUBSTRING(@memo,2,1) = '0'
       begin
       if exists
       (select top 1 * from #triple where item = 'push_ups' and @pushup >= [Standard] order by [standard] desc )
       begin
         set @pushupscore = 
       (select top 1 [score] from #triple where item = 'push_ups' and @pushup >= [Standard] order by [standard] desc)
       end
     else
     begin
      set @pushupscore = (select min([score]) from #triple where item = 'push_ups')
     end
  end
     else
     --2016-11-30這裡判斷式要改，替代項目要加入成績
     begin
     set @repl = SUBSTRING(@memo,2,1)
     
     --新版--
     if @repl = 'F' or @repl = 'G'
     begin
     if exists --判斷有沒有達到最低標準
         (select top 1 * from #repl where item = @repl and @pushup <= [standard] order by [standard])
         begin
         set @pushupscore = (select top 1 [score] from #repl where item = @repl and @pushup <= [standard] order by [standard])
         end
         else  --如果沒達到最低標準則給予最低分
         begin
         set @pushupscore = (select min([score]) from #repl where item = @repl)
         end
     end
     else
     begin
     if exists --判斷有沒有達到最低標準
         (select top 1 * from #repl where item = @repl and @pushup >= [standard] order by [standard] desc)
         begin
         set @pushupscore = (select top 1 [score] from #repl where item = @repl and @pushup >= [standard] order by [standard] desc)
         end
         else  --如果沒達到最低標準則給予最低分
         begin
         set @pushupscore = (select min([score]) from #repl where item = @repl)
         end
     end
         
    end
  end
  
     /* item run */
     if @run is null or @run = 9999
     begin
       set @repl = SUBSTRING(@memo,3,1)
        --if @repl = '0' or @repl = 'G' or @repl = 'F'
       if @repl in('0','F','G')
         set @runscore = 9999
       else
         set @runscore = 999
     end
     else
     begin
     if SUBSTRING(@memo,3,1) = '0' --and @op_id='0'
       begin
       if exists 
       (select top 1 * from #triple where item = 'run' and @run <= [standard] order by [standard])
         begin
           set @runscore = 
           (select top 1 [score] from #triple where item = 'run' and @run <= [standard] order by [standard])
         end
       else
       begin
         set @runscore = (select min([score]) from #triple where item = 'run')
       end
       end
       
     else
     --2016-11-30這裡判斷式要改，替代項目要加入成績
     begin
     set @repl = SUBSTRING(@memo,3,1)
     
     if @repl = 'F' or @repl = 'G'
     begin
     if exists --判斷有沒有達到最低標準
         (select top 1 * from #repl where item = @repl and @run <= [standard] order by [standard] )
         begin
         set @runscore = (select top 1 [score] from #repl where item = @repl and @run <= [standard] order by [standard])
         end
         else  --如果沒達到最低標準則給予最低分
         begin
         set @runscore = (select min([score]) from #repl where item = @repl)
         end
     end
     else
     begin
     if exists --判斷有沒有達到最低標準
         (select top 1 * from #repl where item = @repl and @run >= [standard] order by [standard] desc)
         begin
         set @runscore = (select top 1 [score] from #repl where item = @repl and @run >= [standard] order by [standard] desc)
         end
         else  --如果沒達到最低標準則給予最低分
         begin
         set @runscore = (select min([score]) from #repl where item = @repl)
         end
     end
         
         
   end
   end
     --上面次數及秒數換算成績結束--
     
     /* drop temp table */
     drop table #triple
     drop table #repl
     
     /* decide status */
     if @situpscore >= 60 and @pushupscore >= 60 and @runscore >= 60
     begin
     
     --新版
     if(@situpscore in('999','9999') and @pushupscore not in('999','9999') and @runscore not in('999','9999')) or
       (@situpscore not in('999','9999') and @pushupscore in('999','9999') and @runscore not in('999','9999')) or
       (@situpscore not in('999','9999') and @pushupscore not in('999','9999') and @runscore in('999','9999'))
        set @status = '105'
     else if (@situpscore not in('999','9999') and @pushupscore not in('999','9999') and @runscore not in('999','9999'))
     set @status='102'
      else
        set @status = '103'
     end
     else
     begin
       set @status = '103'
     end
     
     /* update and return result */
     update Result set [status] = @status,[sit_ups_score] = @situpscore,
     [push_ups_score] = @pushupscore,[run_score] = @runscore
     where [date] = @date and [LF_Tag_ID] = @lf_tag
     select [sid]
      ,[id]
      ,[name]
      ,[dbo].[F_GetROCDate]([birth]) 'birth'
      ,[age]
      ,[gender]
      ,dbo.F_GetUnitTitle([unit_code]) 'unit_code'
      ,dbo.F_GetRank([rank_code]) 'rank_code'
      ,[height]
      ,[weight]
      ,CONVERT(decimal(8, 1), [BMI]) as  [BMI]
      ,CONVERT(decimal(8, 1), [bodyfat]) as [bodyfat] 
      ,[sit_ups]
      ,[sit_ups_score]
      ,[push_ups]
      ,[push_ups_score]
      ,[run]
      ,[run_score]
      ,[dbo].[F_GetROCDate]([date]) 'date'
      ,dbo.F_GetCenterName([center_code]) 'center_name'
      ,[result]
      ,[status]
      ,[op_id]
      ,[clothesNum]
      ,[LF_Tag_ID]
      ,[UHF_Tag_ID]
      ,[code]
      ,[memo]
      from Result where [date] = @date and [LF_Tag_ID] = @lf_tag
     
     
     end
     
   end
   
end
else
begin
   select 'further date or no record' as 'error'
end


