type Args = {
  status: "error" | "pending" | "success";
};

function ApiStatus({ status }: Readonly<Args>): JSX.Element {
  switch (status) {
    case "error":
      return <div>Error communication with the data backend</div>;
    case "pending":
      return <div>Loading...</div>;
    default:
      throw Error("Unknown API state");
  }
}

export default ApiStatus;
