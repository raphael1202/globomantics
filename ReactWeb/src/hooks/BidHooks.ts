import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import Problem from "../types/problem";
import axios, { AxiosError, AxiosResponse } from "axios";
import config from "../config";
import { Bid } from "../types/bid";

function useFetchBids(houseId: number) {
  return useQuery<Bid[], AxiosError<Problem>>({
    queryKey: ["bids", houseId],
    queryFn: () =>
      axios
        .get(`${config.baseApiUrl}/house/${houseId}/bids`)
        .then((res) => res.data),
  });
}

function useAddBid() {
  const queryClient = useQueryClient();

  return useMutation<AxiosResponse, AxiosError<Problem>, Bid>({
    mutationFn: (bid: Bid) =>
      axios.post(`${config.baseApiUrl}/house/${bid.houseId}/bids`, bid),
    onSuccess: (_, bid: Bid) => {
      queryClient.invalidateQueries({ queryKey: ["bids", bid.houseId] });
    },
  });
}

export { useFetchBids, useAddBid };
