class BriefingContent():
    # 公司名称
    CompanyName=""
    # 举办学校
    School=""
    # 举办教学楼
    Held=""
    # 宣讲时间
    StartTime=""
    # 发布时间
    PublishTime=""
    # 宣讲正文
    Body=""
    # 操作链接
    OpthonList=""
    # 举办城市
    City=""

    def __init__(self, company,city,school,held,starttime,publishtime,body,optionlist):
        self.CompanyName=company
        self.City=city
        self.School=school
        self.Held=held
        self.StartTime=starttime
        self.PublishTime=publishtime
        self.Body=body
        self.OpthonList=optionlist

