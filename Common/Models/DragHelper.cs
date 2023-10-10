namespace Common.Models
{
    public class DragHelper
    {
        public Board? DraggedTaskBoard { get; private set; } = null;
        public TaskItem? DraggedTask { get; private set; } = null;

        public Board? InsertAtBoard { get; private set; } = null;
        public int InsertAtBoardPosition { get; private set; } = -1;

        public void dragStart(Board board, TaskItem task)
        {
            DraggedTaskBoard = board;
            DraggedTask = task;
            InsertAtBoard = null;
            InsertAtBoardPosition = -1;
        }
        public void dragEnd()
        {
            DraggedTaskBoard = null;
            DraggedTask = null;
            InsertAtBoard = null;
            InsertAtBoardPosition = -1;
        }
        public void drop(Board board, int insertAtBoardPosition)
        {
            if (DraggedTask == null || DraggedTaskBoard == null) return;

            int positionToRemoveFrom = DraggedTaskBoard.Tasks.FindIndex(t => t.Id == DraggedTask.Id);
            if (positionToRemoveFrom == -1)
            {
                DraggedTask = null;
                DraggedTaskBoard = null;
                return;
            }

            if (board.Id == DraggedTaskBoard.Id && positionToRemoveFrom < insertAtBoardPosition)
            {
                insertAtBoardPosition--;
            }

            DraggedTaskBoard.Tasks.Remove(DraggedTask);
            board.Tasks.Insert(insertAtBoardPosition, DraggedTask);
            DraggedTask = null;
        }

        public void dragEnter(Board board, int insertAtBoardPosition)
        {
            InsertAtBoard = board;
            InsertAtBoardPosition = insertAtBoardPosition;
        }
        public void dragLeave()
        {
            InsertAtBoard = null;
            InsertAtBoardPosition = -1;
        }
    }
}