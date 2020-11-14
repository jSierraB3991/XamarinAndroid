namespace LoadMore.Model
{
    internal class Response<T>
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }
    }
}