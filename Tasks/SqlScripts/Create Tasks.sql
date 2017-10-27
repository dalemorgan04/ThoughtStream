CREATE TABLE [dbo].[Tasks] (
    [TaskId]      INT           IDENTITY (1, 1) NOT NULL,
    [UserId]      INT           NOT NULL,
    [Description] NVARCHAR (50) NULL,
    [PriorityId]  INT           NULL,
    [TimeFrameId] INT           NULL,
    [DateTime]    DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([TaskId] ASC),
    CONSTRAINT [FK_Tasks_ToPriority] FOREIGN KEY ([PriorityId]) REFERENCES [dbo].[Priority] ([PriorityId]),
    CONSTRAINT [FK_Tasks_ToUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Due class converts this filed to correct timeframe',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Tasks',
    @level2type = N'COLUMN',
    @level2name = N'DateTime'